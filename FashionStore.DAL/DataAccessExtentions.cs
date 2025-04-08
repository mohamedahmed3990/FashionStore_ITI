using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Context;
using FashionStore.DAL.Interfaces;
using FashionStore.DAL.Repositories;
using FashionStore.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FashionStore.DAL.UnitOfWork;

namespace FashionStore.DAL
{
    public static class DataAccessExtentions
    {
        public static void AddDataAccessService(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IParentCategoryRepository, ParentCategoryRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("defualtConnection"));
            });
        }
    }
}
