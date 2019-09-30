using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using MongoDB.Driver;

namespace ITG.Brix.Teams.Infrastructure.Providers
{
    public interface ITeamOdataProvider : IBaseOdataProvider<TeamClass>
    {
        FilterDefinition<TeamClass> GetFilterDefinition(string filter);
    }
}
