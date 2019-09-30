using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class LayoutClassMap : DomainClassMap<LayoutClass>
    {
        public override void Map(BsonClassMap<LayoutClass> cm)
        {
            cm.AutoMap();
        }
    }
}
