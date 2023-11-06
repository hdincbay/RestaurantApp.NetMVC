using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Restaurant.Models;
using Services;
using Services.Contracts;

namespace Restaurant.Infrastructe.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(option => {
                option.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("Restaurant"));
            });
        }
        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddSession(option => {
                option.IdleTimeout = TimeSpan.FromMinutes(10);//Oturum bilgilerini 10 dk tut taze istek gelmediyse düşür.
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<Cart>(c => SessionCart.GetCart(c));

        }
        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IOrderService, OrderManager>();
        }
    }
}