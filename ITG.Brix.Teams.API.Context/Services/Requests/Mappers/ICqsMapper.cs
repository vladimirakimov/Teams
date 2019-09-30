using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Mappers
{
    public interface ICqsMapper
    {
        ListTeamQuery Map(ListTeamRequest request);
        GetTeamQuery Map(GetTeamRequest request);
        UpdateTeamCommand Map(UpdateTeamRequest request);
        DeleteTeamCommand Map(DeleteTeamRequest request);
        CreateTeamCommand Map(CreateTeamRequest request);
        ListOperatorQuery Map(ListOperatorRequest request);
        ListDriverWaitQuery Map(ListDriverWaitRequest request);
    }
}
