using Microsoft.Extensions.DependencyInjection;
using TriAngleUi.Models;

namespace TriAngleUi
{
    public static class DependencyBuilding
    {
        public static ServiceProvider Build(string file)
        {
            var serviceProvider = new ServiceCollection();
            serviceProvider.AddScoped<ISummation>(_ => new Summation(file));

            var services = serviceProvider.BuildServiceProvider();

            return services;
        }
    }
}