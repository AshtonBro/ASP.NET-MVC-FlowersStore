using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using FlowersStore.DataAccess.MSSQL.Entities;
using FlowersStore.DataAccess.MSSQL;
using FlowersStore.Core.Services;
using FlowersStore.BusinessLogic;
using FlowersStore.Core.Repositories;
using FlowersStore.DataAccess.MSSQL.Repositories;
using System;

namespace FlowersStore.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<WebUIMappingProfile>();
                cfg.AddProfile<DataAccessMappingProfile>();
            });

            services.AddHttpContextAccessor();

            services.AddControllersWithViews();

            services.AddDbContext<FlowersStoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("FlowersStoreDbContext"));

            }).AddIdentity<User, UserRole>(config => 
            {
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;

            })
            .AddEntityFrameworkStores<FlowersStoreDbContext>()

            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Home/AccessDenied";
                config.AccessDeniedPath = "/Home/AccessDenied";
            });

            services.AddAuthorization(options => 
            {
                options.AddPolicy(ClaimPolicyMatch.ADMIN, builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, ClaimPolicyMatch.ADMIN);
                });

                options.AddPolicy(ClaimPolicyMatch.USER, builder =>
                {
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, ClaimPolicyMatch.USER) 
                                                || x.User.HasClaim(ClaimTypes.Role, ClaimPolicyMatch.ADMIN));
                });
            });

            services.Configure<IdentityOptions>(cfg =>
            {
                // Lockout settings
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                // user settings
                cfg.User.RequireUniqueEmail = true;
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductCardService, ProductCardService>();
            services.AddScoped<IProductCardRepository, ProductCardRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
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
                    name: "Areas",
                    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}