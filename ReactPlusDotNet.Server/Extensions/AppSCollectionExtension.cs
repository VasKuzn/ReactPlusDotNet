using ReactPlusDotNet.Server.Interfaces;
using ReactPlusDotNet.Server.Storage;

namespace ReactPlusDotNet.Server.Extensions
{
    public static class AppSCollectionExtension
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection services, ConfigurationManager cm) 
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            var stringConnection = cm.GetConnectionString("SqliteConnectionString");
            services.AddSingleton<IStorage>(new SqliteStorage(stringConnection));
            services.AddCors(
                opt => opt.AddPolicy("CorsPolicy", policy => { policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:5173"); }) // если на другой комп и в другой среде, то пишем dotnet run https://localhost:5173 а в withorigins(args[0])
                );
            

            return services;
        }
    }
}
