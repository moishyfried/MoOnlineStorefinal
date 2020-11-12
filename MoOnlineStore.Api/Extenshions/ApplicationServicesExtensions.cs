using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using MoOnlineStore.Api.Errors;
using MoOnlineStore.Core.Interfaces;
using MoOnlineStore.Infrastructure;
using MoOnlineStore.Infrastructure.DataBase;
using MoOnlineStore.Infrastructure.Services;
using System.Linq;

namespace MoOnlineStore.Api.Extenshions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IResponseCachedService, ResponseCachedService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IpaymentService, PaymentService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));
             services.AddScoped<IBasketRepository,BasketRepository>();
              services.Configure<ApiBehaviorOptions>(options => 
              {
               options.InvalidModelStateResponseFactory = ActionContext =>
               {
                 var errors = ActionContext.ModelState
                 .Where( e => e.Value.Errors.Count > 0)
                 .SelectMany(x => x.Value.Errors)
                 .Select(p => p.ErrorMessage).ToArray();

                 var errorResponse = new ApiValidationErrorResponse
                 {
                    Errors = errors
                 };
                 return new BadRequestObjectResult(errorResponse);
               };
              });
            return services;
        }
    }
}