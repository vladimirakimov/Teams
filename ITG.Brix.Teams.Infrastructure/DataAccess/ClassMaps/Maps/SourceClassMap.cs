using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class SourceClassMap : DomainClassMap<Source>
    {
        public override void Map(BsonClassMap<Source> cm)
        {
            cm.AutoMap();
        }
    }
}
