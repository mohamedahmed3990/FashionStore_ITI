using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Interfaces;
using FashionStore.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FashionStore.DAL
{
    public static class DataAccessExtentions
    {
        public static void AddDataAccessService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

        }
    }
}
