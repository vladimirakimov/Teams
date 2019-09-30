using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models
{
    public class DeleteTeamRequest
    {
        private readonly DeleteTeamFromRoute _route;
        private readonly DeleteTeamFromQuery _query;
        private readonly DeleteTeamFromHeader _header;

        public DeleteTeamRequest(DeleteTeamFromRoute route,
                                 DeleteTeamFromQuery query,
                                 DeleteTeamFromHeader header)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _header = header ?? throw new ArgumentNullException(nameof(header));
        }

        public string RouteId => _route.Id;

        public string QueryApiVersion => _query.ApiVersion;

        public string HeaderIfMatch => _header.IfMatch;
    }
}
