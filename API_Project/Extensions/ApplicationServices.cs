using API_Project.Error;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using Services;
using StackExchange.Redis;
using System.Reflection;

namespace API_Project.Extensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection ApplicationService(this IServiceCollection Services , IConfiguration configuration)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<TokenService>();

            Services.AddSingleton<IConnectionMultiplexer>(opt =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection"));
                return ConnectionMultiplexer.Connect(config);
            });


            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(e => e.Value.Errors.Any())
                    .SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage).ToList();

                    return new BadRequestObjectResult(new ValidationResponse() { Errors = errors });
                };
            });
            return Services;
        }
    }
}
