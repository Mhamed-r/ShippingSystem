using Microsoft.EntityFrameworkCore;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Infrastructure.Data;
using Shipping.Api.Infrastructure.Repositories;
using Shipping.Api.Services;
namespace Shipping.Api;
public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration Configuration)
    {
        string txt = "";
        services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();
        services.AddDbContext<ShippingContext>(options =>
        {
            options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddCors(options =>
        {
            options.AddPolicy(txt,
            builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
        });
        services.AddAutoMapper(typeof(MapperConfig));
        services.AddScoped(typeof(IGenericRepository<,>),typeof(GenericRepository<,>));
        services.AddScoped<CourierReportsService>();
        services.AddScoped<RegionService>();
        return services;
    }
}
