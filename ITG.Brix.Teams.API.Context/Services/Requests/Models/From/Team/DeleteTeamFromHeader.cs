using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models.From
{
    public class DeleteTeamFromHeader
    {
        [FromHeader(Name = "If-Match")]
        public string IfMatch { get; set; }
    }
}
