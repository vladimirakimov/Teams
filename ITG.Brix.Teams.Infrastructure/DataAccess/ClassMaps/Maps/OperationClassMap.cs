using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class OperationClassMap : DomainClassMap<Operation>
    {
        public override void Map(BsonClassMap<Operation> cm)
        {
            cm.AutoMap();
        }
    }
}
