using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models
{
    public class ListDriverWaitRequest
    {
        private readonly ListDriverWaitFromQuery _query;

        public ListDriverWaitRequest(ListDriverWaitFromQuery query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string QueryApiVersion => _query.ApiVersion;
    }
}
