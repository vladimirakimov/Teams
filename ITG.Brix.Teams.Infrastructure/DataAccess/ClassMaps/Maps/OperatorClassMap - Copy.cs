using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class OperatorClassModelMap : DomainClassMap<OperatorClassModel>
    {
        public override void Map(BsonClassMap<OperatorClassModel> cm)
        {
            cm.AutoMap();
        }
    }
}
