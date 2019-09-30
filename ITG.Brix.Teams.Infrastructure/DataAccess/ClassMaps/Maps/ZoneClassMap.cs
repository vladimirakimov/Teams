using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class ZoneClassMap : DomainClassMap<Zone>
    {
        public override void Map(BsonClassMap<Zone> cm)
        {
            cm.AutoMap();
        }
    }
}
