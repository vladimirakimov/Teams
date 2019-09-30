using MongoDB.Driver;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.Configurations
{
    public interface IPersistenceContext
    {
        IMongoDatabase Database { get; }
    }
}
