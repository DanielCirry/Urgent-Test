using Microsoft.Extensions.DependencyInjection;
using Urgent.Application.Views;
using Urgent.Domain.Contracts;
using Urgent.Domain.Services;

namespace Urgent.Application
{
    public class Program
    {
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IUrgentService, UrgentService>();

            services.AddTransient<UrgentApplication>();
            return services;
        }

        public static void Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<UrgentApplication>().Run();
        }

    }
}
