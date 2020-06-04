using API.Utilities;
using BusinessLayer;
using BusinessLayer.SignalR;
using BusinessLayer.Utilities;
using DataAccessLayer;
using DataAccessLayer.ClassesRepository;
using DataAccessLayer.InterfacesRepository;
using EntityLayer.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
namespace API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _evn;
        public Startup(IWebHostEnvironment evn)
        {
            _evn = evn;
            Configuration = new ConfigurationBuilder()
           .SetBasePath(_evn.ContentRootPath)
           .AddJsonFile($"appsettings.json").Build();
            //ConnectionStringHelper.Connectionstring = Configuration
            //.GetConnectionString("DefaultConnection");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //// Add Controller Service
            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<BusinessWrapper>();
            // Add framework services.
            services.AddMvc(o =>
            {
                o.Conventions.Add(new AddControllerFiltersConvention());
            });

            //brotli compression
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<Utilities.BrotliCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {
                "application/xhtml+xml",
                "application/atom+xml",
                "image/svg+xml",
            });
            });

            services.AddControllers()
               .AddNewtonsoftJson();
            // Add DBContext
            services.AddDbContext<GrubNowDbContext>(option =>
                               option.UseLazyLoadingProxies()
             .UseSqlServer(ConnectionStringHelper.Connectionstring));

            //add token based authentication
            services.ConfigureJwtAuthentication();
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build();
            });

            // SignalR Service
            services.AddTransient<SignalRService>();
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            //HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IListOfAllData, ListOfAllDataReposity>();
            services.AddTransient<IEfRepository, EfRepository>();

            // Identity
            services.AddIdentity<AppUser, AppRole>(
                   option =>
                   {
                       option.Password.RequireDigit = false;
                       option.Password.RequiredLength = 6;
                       option.Password.RequireNonAlphanumeric = false;
                       option.Password.RequireUppercase = false;
                       option.Password.RequireLowercase = false;
                   }
               ).AddEntityFrameworkStores<GrubNowDbContext>()
             .AddDefaultTokenProviders();

            //Add Cookies 
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            });

            //Add MVC Sevice
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMemoryCache();

            //Data Seeder Service middleware
            //DataSeeder.serviceProvider = services.BuildServiceProvider();

            //services.AddOpenApiDocument();
            services.AddOpenApiDocument(document =>
            {
                document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
                //      new OperationSecurityScopeProcessor("JWT"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GrubNowDbContext ctx,
        UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {

            //Add Cors and Ogiins From request To acces Date 
            List<string> origins = new List<string> { "http://localhost:4200", "https://localhost:4200" };
            app.UseCors(options => options
            .WithOrigins(origins.ToArray())
            .AllowAnyMethod().AllowCredentials().AllowAnyHeader().SetIsOriginAllowed((host) => true));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //Use Swagger 
            //app.UseSwagger();
            ////Use Swagger UI
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PLENUM Project V1");
            //});

            app.UseRouting();

            // Add Athentication And Authorizationm
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //Used for logging using ILogger and SeriLog
            //var t = Configuration["Environment"];
            //if (Configuration["Environment"] == "Local" || Configuration["Environment"] == "Dev")
            //{
            //    app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            //}

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //Use SignalR In Core 3.0.0
                endpoints.MapHub<SignalRHub>(Configuration.GetSection("SignalRPath").Value);
            });
           // DbInitializer.Initialize(ctx, userManager, roleManager);

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc(); // serve ReDoc UI
        }
    }
}
