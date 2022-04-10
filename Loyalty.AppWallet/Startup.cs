using Loyalty.AppWallet.Filters.ErrorFilters;
using Loyalty.DataAccess.Managers;
using Loyalty.DataAccess.Repositories;
using Loyalty.DataAccess.Shared;
using Loyalty.DataAccess.Shared.IManagers;
using Loyalty.Domain.Shared.IRepositories;

using Loyalty.Services.Shared.IManagers;
using Loyalty360Core.Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Loyalty.AppWallet
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
            services.AddDbContext<LoyaltyContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LoyaltyAppWalletCon"),
                    assembly =>
                    {
                        assembly.MigrationsAssembly(typeof(LoyaltyContext).Assembly.FullName);
                    });
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();

            });
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IPassRepository, PassRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IPassManager, PassManager>();
            services.AddScoped<IDeviceManager, DeviceManager>();
            services.AddScoped<IRegistrationManager, RegisterationManager>();
            services.AddScoped<Helper,Helper>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Doc", Version = "v1" });

            });
            services.AddMvc();
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddResponseCaching();

            
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsDevelopment() || Configuration.GetValue<bool>("AppSetting:AllowSwagger"))
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoyaltyAppleWallet");
                    c.RoutePrefix = "swagger";
                });
            }
            app.UseCors(options =>
            {
                options.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials();
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseMiddleware<GlobalExceptionMiddleware>(); 
            app.UseResponseCaching();
            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(10)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(env.ContentRootPath + @"\App-logs"),
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=AppWallet}/{action=Index}/{id?}");
            });


        }
    }
}

