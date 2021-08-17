using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FlowersStore.Core.Services;
using FlowersStore.DataAccess.MSSQL;
using FlowersStore.DataAccess.MSSQL.Entities;
using FlowersStore.DataAccess.MSSQL.SeedDb;

namespace FlowersStore.WebUI
{
    public static class ProjectInitializer
    {
        private const string ADMIN_CONFIG_SECTION = "AdminConfig";

        public static async void InitializeDefaultAdmin(
            this IServiceProvider service, 
            IConfiguration configuration)
        {
            var scope = service.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

            var basketService = (IBasketService)scope.ServiceProvider.GetService(typeof(IBasketService));

            var adminSettings = configuration.GetSection(ADMIN_CONFIG_SECTION).Get<AdminConfig>();

            var admin = await userManager.FindByNameAsync(adminSettings.Name);

            if (admin == null)
            {
                //Create the default Admin account
                var userAdmin = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = adminSettings.Name,
                    UserName = adminSettings.Name,
                    Email = adminSettings.Email,
                    PasswordHash = adminSettings.Password,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(userAdmin, userAdmin.PasswordHash);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(userAdmin, new Claim(ClaimTypes.Role, ClaimPolicyMatch.ADMIN));

                    await basketService.Create(userAdmin.Id);
                }
                else
                {
                    var error = result.Errors.FirstOrDefault().Description;

                    throw new ArgumentException(error);
                }
            }
        }

        public static void InitializeDBdata(this IServiceProvider service)
        {
            var scope = service.CreateScope();

            var context = scope.ServiceProvider.GetService<FlowersStoreDbContext>();

            SeedingRelatedData.Seed(context);
        }
    }
}
