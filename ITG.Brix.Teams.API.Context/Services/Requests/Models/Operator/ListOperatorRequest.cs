using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models
{
    public class ListOperatorRequest
    {
        private readonly ListOperatorFromQuery _query;

        public ListOperatorRequest(ListOperatorFromQuery query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string Filter => _query.Filter;

        public string Skip => _query.Skip;

        public string Top => _query.Top;
    }
}
