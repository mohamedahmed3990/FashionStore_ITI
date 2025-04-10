using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.Services.BasketService;
using FashionStore.BLL.Services.OrderService;
using FashionStore.BLL.Services.ProductService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FashionStore.BLL
{
    public static class BusinessExtentions
    {
        public static void AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBasketService), typeof(BasketService));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IParentCategoryService, ParentCategoryService>();

            services.AddValidatorsFromAssembly(typeof(BusinessExtentions).Assembly);

        }
    }
}
