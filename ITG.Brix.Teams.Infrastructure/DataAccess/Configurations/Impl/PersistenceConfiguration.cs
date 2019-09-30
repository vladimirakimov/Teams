using ITG.Brix.Teams.Infrastructure.Internal;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl
{
    public class PersistenceConfiguration : IPersistenceConfiguration
    {
        private readonly string _connectionString;

        public PersistenceConfiguration(string connectionString)
        {
            _connectionString = connectionString ?? throw Error.ArgumentNull(nameof(connectionString));
        }

        public string ConnectionString => _connectionString;

        public string Database => "Brix-Teams";

    }
}
