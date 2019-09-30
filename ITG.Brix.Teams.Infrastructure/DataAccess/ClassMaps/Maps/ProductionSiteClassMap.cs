using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class ProductionSiteClassMap : DomainClassMap<ProductionSite>
    {
        public override void Map(BsonClassMap<ProductionSite> cm)
        {
            cm.AutoMap();
        }
    }
}
