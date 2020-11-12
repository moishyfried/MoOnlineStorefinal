using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MoOnlineStore.Api.Extenshions;
using MoOnlineStore.Api.Middeware;
using MoOnlineStore.Core.Helpers;
using MoOnlineStore.Core.Interfaces;
using MoOnlineStore.Infrastructure;
using MoOnlineStore.Infrastructure.Identity;
using StackExchange.Redis;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace MoOnlineStore.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;    
        }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<StoreContext>(x =>
            {
                x.UseSqlServer(_config.GetConnectionString("azureDefaultConnection"));
            });
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseSqlServer(_config.GetConnectionString("azureDefaultConnection"));
            });
            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<StoreContext>(x =>
            {
                x.UseSqlServer(_config.GetConnectionString("azureDefaultConnection"));
            });
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseSqlServer(_config.GetConnectionString("azureDefaultConnection"));
            });
            ConfigureServices(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = new ConfigurationOptions
                {
                    EndPoints = { "redis-14281.c56.east-us.azure.cloud.redislabs.com:14281" },
                    Password = "NvZ26iH8uhfoUMR84fbHIxuzIe37PdmD",
                    Ssl = false
                };
                return ConnectionMultiplexer.Connect(options);
            });
            services.AddApplicationServices();
            services.AddIdentityServices(_config);
            services.AddSwaggerDocumentation();
        
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                 policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:44352").WithOrigins("https://localhost:4200")
                );
            });
        }
     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddelware>();
            
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
                {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                     RequestPath = "/Content"
            });
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
