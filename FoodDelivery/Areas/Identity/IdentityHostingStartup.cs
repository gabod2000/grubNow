using System;
using DataAccessLayer;
using FoodDelivery.Constant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;

[assembly: HostingStartup(typeof(FoodDelivery.Areas.Identity.IdentityHostingStartup))]
namespace FoodDelivery.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<GrubNowDbContext>(options =>
                    options.UseSqlServer(Constants.ConnectionString));

                services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).
                 AddRoles<AppRole>()
                .AddEntityFrameworkStores<GrubNowDbContext>();

            });
        }
    }
}