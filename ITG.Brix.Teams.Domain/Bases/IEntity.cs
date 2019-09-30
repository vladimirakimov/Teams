namespace ITG.Brix.Teams.Domain.Bases
{
    public interface IEntity
    {
        IIdentity GetIdentity();
    }

    public interface IEntity<out TIdentity> : IEntity where TIdentity : IIdentity
    {
        TIdentity Id { get; }
    }
}
