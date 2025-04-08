﻿using FashionStore.DAL.Context;
using FashionStore.DAL.Interfaces;
using FashionStore.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionStore.DAL
{
    public static class DataAccessExtentions
    {
        public static void AddDataAccessService(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<AppDbContext>(options =>
                options
                    .LogTo(Console.WriteLine)
                    .UseSqlServer(connectionString));
    
                services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
