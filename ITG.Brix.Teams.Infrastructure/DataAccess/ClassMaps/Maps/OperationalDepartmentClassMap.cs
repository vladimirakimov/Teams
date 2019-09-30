using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class OperationalDepartmentClassMap : DomainClassMap<OperationalDepartment>
    {
        public override void Map(BsonClassMap<OperationalDepartment> cm)
        {
            cm.AutoMap();
        }
    }
}
