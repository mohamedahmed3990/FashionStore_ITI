using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Context;
using FashionStore.DAL.Interfaces;
using FashionStore.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionStore.DAL
{
    public static class DataAccessExtentions
    {
        public static void AddDataAccessService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("defualtConnection"));
            });
        }
    }
}
