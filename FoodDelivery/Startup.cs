using System;
using DataAccessLayer;
using DataAccessLayer.ClassesRepository;
using DataAccessLayer.InterfacesRepository;
using FoodDelivery.Constant;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;

namespace FoodDelivery
{
    public class Startup
    {
        private readonly IWebHostEnvironment _evn;
        private readonly IConfigurationRoot _config;

        public Startup(IWebHostEnvironment evn)
        {
            _evn = evn;
            // Add Connection String File Json 
            var builder = new ConfigurationBuilder().SetBasePath(_evn.ContentRootPath)
                .AddJsonFile($"appsettings.{_evn.EnvironmentName}.json");

            _config = builder.Build();
            Constants.ConnectionString= _config.GetSection("ConnectionStrings:LearningDbConnectionString").Value;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Add Configration 
            services.AddSingleton(_config);

            // Add Service Of LearningDbContext Class
            services.AddDbContext<GrubNowDbContext>();
            services.AddTransient<IListOfAllData, ListOfAllDataReposity>();
            services.AddTransient<IEfRepository, EfRepository>();

            // Add Identity Option Related To Password and Email
            services.Configure<IdentityOptions>(Options =>
            {
                Options.User.RequireUniqueEmail = true;
                Options.Password.RequireDigit = false;
                Options.Password.RequiredLength = 8;
                Options.Password.RequiredUniqueChars = 0;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireNonAlphanumeric = false;

            });

            // Add Cookies In Your Application 
            services.ConfigureApplicationCookie(config => {
                config.AccessDeniedPath = "";
                config.Cookie.HttpOnly = true;
                config.Cookie.Name = "Learning";
                config.LoginPath = "/Identity/Account/Login";
                config.LogoutPath = "/Account/SignOut";
                config.ReturnUrlParameter = "ReturnUrl";
                config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

            //
            services.AddAuthentication()
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                    _config.GetSection("Authentication:Google");

                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            })
            .AddFacebook(microsoftOptions =>
            {
                microsoftOptions.AppId = _config["Authentication:Facebook:AppId"];
                microsoftOptions.AppSecret = _config["Authentication:Facebook:AppSecret"];
            });

            services.AddRazorPages();

            // Add Application File Related To Data Base 
            services.AddTransient<IEfRepository, EfRepository>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc().AddControllersAsServices()
               .AddRazorRuntimeCompilation()
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Home}/{action=PublicSite}/{id?}");
                    pattern: "{controller=Home}/{action=PublicSiteLandingPage}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "areas", "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
