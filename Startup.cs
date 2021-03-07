using FlowersStore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FlowersStore.Services;
using FlowersStore.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using FlowersStore.Entities;
using FlowersStore.Services.ServicesInterfaces;
using System;

namespace FlowersStore
{
    public class Startup
    {
        private const string ADMIN = "Administrator";
        private const string USER = "User";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<StoreDBContext>(options =>
            {
                options.UseSqlServer("name=ConnectionStrings:DefaultConnection");
            }).AddIdentity<User, UserRole>(config => 
            {
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;

            })
            .AddEntityFrameworkStores<StoreDBContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Home/AccessDenied";
                config.AccessDeniedPath = "/Home/AccessDenied";
            });

            services.AddAuthorization(options => 
            {
                options.AddPolicy(ADMIN, builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, ADMIN);
                });

                options.AddPolicy(USER, builder =>
                {
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, USER) 
                                                || x.User.HasClaim(ClaimTypes.Role, ADMIN));
                });
            });

            services.AddScoped<IShopingCartCRUDService<ShopingCart>, ShopingCartService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserService>();
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

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}