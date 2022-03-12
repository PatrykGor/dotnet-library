using System;
using Biblioteka.Repository;

namespace Biblioteka.Repository.DI
{
    public static class DbConnectionsServiceCollectionExtensions
    {
        public static IServiceCollection AddDbConnections(this IServiceCollection collection, IConfiguration config)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (config == null) throw new ArgumentNullException(nameof(config));

            collection.Configure<ApplicationDbContextOptions>(config);
            return collection.AddDbContext<ApplicationDbContext>();
        }
    }
}
