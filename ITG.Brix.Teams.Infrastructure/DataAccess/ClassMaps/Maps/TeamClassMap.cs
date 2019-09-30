using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class TeamClassMap : DomainClassMap<TeamClass>
    {
        public override void Map(BsonClassMap<TeamClass> cm)
        {
            cm.AutoMap();
        }
    }
}
