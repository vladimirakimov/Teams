using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models
{
    public class CreateTeamRequest
    {
        private readonly CreateTeamFromQuery _query;
        private readonly CreateTeamFromBody _body;

        public CreateTeamRequest(CreateTeamFromQuery query,
                                 CreateTeamFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string Name => _body.Name;

        public string Image => _body.Image;

        public string Description => _body.Description;

        public string Layout => _body.Layout;
    }
}
