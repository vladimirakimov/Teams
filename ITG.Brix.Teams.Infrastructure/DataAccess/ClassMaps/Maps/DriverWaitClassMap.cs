using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class DriverWaitClassMap : DomainClassMap<DriverWait>
    {
        public override void Map(BsonClassMap<DriverWait> cm)
        {
            cm.AutoMap();
        }
    }
}
