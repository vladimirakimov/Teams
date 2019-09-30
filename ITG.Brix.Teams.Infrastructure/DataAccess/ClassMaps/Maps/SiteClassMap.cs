using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class SiteClassMap : DomainClassMap<Site>
    {
        public override void Map(BsonClassMap<Site> cm)
        {
            cm.AutoMap();
        }
    }
}
