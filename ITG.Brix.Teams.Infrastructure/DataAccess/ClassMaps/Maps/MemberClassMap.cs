using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class MemberClassMap : DomainClassMap<MemberClass>
    {
        public override void Map(BsonClassMap<MemberClass> cm)
        {
            cm.AutoMap();
        }
    }
}
