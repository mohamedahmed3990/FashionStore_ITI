using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.Services.BasketService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FashionStore.BLL
{
    public static class BusinessExtentions
    {
        public static void AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBasketService), typeof(BasketService));

            services.AddValidatorsFromAssembly(typeof(BusinessExtentions).Assembly);

        }
    }
}
