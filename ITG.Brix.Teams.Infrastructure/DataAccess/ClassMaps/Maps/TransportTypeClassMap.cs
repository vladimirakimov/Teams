using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class TransportTypeClassMap : DomainClassMap<TransportType>
    {
        public override void Map(BsonClassMap<TransportType> cm)
        {
            cm.AutoMap();
        }
    }
}
