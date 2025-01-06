using ReactPlusDotNet.Server.Interfaces;
using ReactPlusDotNet.Server.Seed;
using ReactPlusDotNet.Server.Storage;

namespace ReactPlusDotNet.Server.Extensions
{
    public static class AppSProviderExtension
    {
        public static IServiceProvider AddCustomService(this IServiceProvider services, IConfiguration configuration) 
        {
            using var scope = services.CreateScope();

            var storage = scope.ServiceProvider.GetService<IStorage>();

            var dbStorage = storage as SqliteStorage;

            if (dbStorage != null)
            {
                string connectionString = configuration.GetConnectionString("SqliteConnectionString");

                new FakerInitializer(connectionString).Initialize();
            }

            return services;
        }
    }
}
