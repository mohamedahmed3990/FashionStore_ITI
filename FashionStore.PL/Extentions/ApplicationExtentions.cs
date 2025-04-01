namespace FashionStore.PL.Extentions
{
    public static class ApplicationExtentions
    {
        public static void AddSwaggerService(this IServiceCollection services) 
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

    }
}
