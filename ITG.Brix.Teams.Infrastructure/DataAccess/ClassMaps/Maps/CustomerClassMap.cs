using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class CustomerClassMap : DomainClassMap<Customer>
    {
        public override void Map(BsonClassMap<Customer> cm)
        {
            cm.AutoMap();
        }
    }
}
