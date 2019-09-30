using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using System;
using System.Collections.Generic;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models
{
    public class UpdateTeamRequest
    {
        private readonly UpdateTeamFromHeader _header;
        private readonly UpdateTeamFromQuery _query;
        private readonly UpdateTeamFromRoute _route;
        private readonly UpdateTeamFromBody _body;

        public UpdateTeamRequest(UpdateTeamFromHeader header,
                                 UpdateTeamFromQuery query,
                                 UpdateTeamFromRoute route,
                                 UpdateTeamFromBody body)
        {
            _header = header ?? throw new ArgumentNullException(nameof(header));
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string RouteId => _route.Id;

        public string QueryApiVersion => _query.ApiVersion;

        public string HeaderIfMatch => _header.IfMatch;

        public string HeaderContentType => _header.ContentType;

        public string BodyPut => _body.Put;

        public string Name => _body.Name;

        public string Image => _body.Image;

        public string Description => _body.Description;

        public string DriverWait => _body.DriverWait;

        public string Layout => _body.Layout;

        public List<Guid> Members => _body.Members;

        public string FilterContent => _body.FilterContent;
    }
}
