using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models.From
{
    public class GetTeamFromQuery
    {
        [FromQuery(Name = "api-version")]
        public string ApiVersion { get; set; }
    }
}
