using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models
{
    public class GetTeamRequest
    {
        private readonly GetTeamFromRoute _route;
        private readonly GetTeamFromQuery _query;

        public GetTeamRequest(GetTeamFromRoute route,
                          GetTeamFromQuery query)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _query = query ?? throw new ArgumentNullException(nameof(route));
        }

        public string RouteId => _route.Id;

        public string QueryApiVersion => _query.ApiVersion;
    }
}
