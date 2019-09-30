using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models.From
{
    public class GetTeamFromRoute
    {
        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
