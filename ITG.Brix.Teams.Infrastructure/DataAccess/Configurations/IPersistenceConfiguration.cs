namespace ITG.Brix.Teams.Infrastructure.DataAccess.Configurations
{
    public interface IPersistenceConfiguration
    {
        string ConnectionString { get; }
        string Database { get; }
    }
}
