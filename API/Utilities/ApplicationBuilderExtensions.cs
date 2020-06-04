using BusinessLayer.DatabaseSubscriptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utilities
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder services, string connectionString)
            where T : IDatabaseSubscription
        {
            var serviceProvider = services.ApplicationServices;
            var subscription = serviceProvider.GetService<T>();
            subscription.Configure(connectionString);
        }

        public static void UpdateDatabase(this IApplicationBuilder services)
        {

            using (var serviceScope = services.ApplicationServices
                        .GetRequiredService<IServiceScopeFactory>()
                     .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<GrubNowDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }


    }
}
