
using FashionStore.BLL;
using FashionStore.DAL;
using FashionStore.DAL.Interfaces;
using FashionStore.DAL.Repositories;
using FashionStore.PL.Extentions;
using FashionStore.PL.Middlewares;
using StackExchange.Redis;

namespace FashionStore.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
            {
                option.SuppressModelStateInvalidFilter = true;
            });


            builder.Services.AddSingleton<IConnectionMultiplexer>(service =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });


            builder.Services.AddBusinessService();
            builder.Services.AddDataAccessService(builder.Configuration);

            builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();
            builder.Services.AddProblemDetails();

            builder.Services.AddSwaggerService();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseExceptionHandler();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
