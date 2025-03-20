
using Microsoft.EntityFrameworkCore;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;
using Shipping.Api.Infrastructure.Repositories;
using Shipping.Api.Services;

namespace Shipping.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<ShippingContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped(typeof(IGenericRepository<,>),typeof(GenericRepository<,>));
            builder.Services.AddScoped<CourierReportsService>();
            builder.Services.AddScoped<RegionService>();



            #endregion
            var app = builder.Build();
            #region Configure MiddleWare
            // Configure the HTTP request pipeline.
            if(app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json","v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(txt);

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
