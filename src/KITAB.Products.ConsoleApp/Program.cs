using Microsoft.Extensions.DependencyInjection;
using KITAB.Products.Application.Notificators;
using KITAB.Products.Application.Products;
using KITAB.Products.Infra.Products;

namespace KITAB.Products.ConsoleApp
{
    class Program
    {
        public static void Main()
        {
            // Create service collection and configure our services
            var services = ConfigureServices();

            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<ConsoleApplication>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddAutoMapper(typeof(AutoMapping));

            services.AddScoped<INotificatorService, NotificatorService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // IMPORTANT: Register our application entry point
            services.AddScoped<ConsoleApplication>();

            return services;
        }
    }
}
