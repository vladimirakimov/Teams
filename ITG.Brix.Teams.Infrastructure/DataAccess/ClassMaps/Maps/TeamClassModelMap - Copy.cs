using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Bases;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class TeamClassModelMap : DomainClassMap<TeamClass>
    {
        public override void Map(BsonClassMap<TeamClass> cm)
        {
            cm.AutoMap();
            cm.MapField("_operations").SetElementName("Operations");
            cm.MapField("_sources").SetElementName("Sources");
            cm.MapField("_sites").SetElementName("Sites");
            cm.MapField("_operationalDepartments").SetElementName("OperationalDepartments");
            cm.MapField("_zones").SetElementName("Zones");
            cm.MapField("_typePlannings").SetElementName("TypePlannings");
            cm.MapField("_customers").SetElementName("Customers");
            cm.MapField("_productionSites").SetElementName("ProductionSites");
            cm.MapField("_transportTypes").SetElementName("TransportTypes");
            cm.MapField("_teamOperators").SetElementName("TeamOperators");
        }
    }
}
