using ITG.Brix.Teams.Infrastructure.Internal;
using MongoDB.Driver;
using System.Security.Authentication;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl
{
    public class PersistenceContext : IPersistenceContext
    {
        private readonly IPersistenceConfiguration _persistenceConfiguration;

        private IMongoDatabase _database;

        public PersistenceContext(IPersistenceConfiguration persistenceConfiguration)
        {
            _persistenceConfiguration = persistenceConfiguration ?? throw Error.ArgumentNull(nameof(persistenceConfiguration));
        }

        public IMongoDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      new MongoUrl(_persistenceConfiguration.ConnectionString)
                    );

                    settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

                    var client = new MongoClient(settings);

                    _database = client.GetDatabase(_persistenceConfiguration.Database);
                }
                return _database;
            }
        }
    }
}
