using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models.From
{
    public class UpdateTeamFromHeader
    {
        [FromHeader(Name = "If-Match")]
        public string IfMatch { get; set; }

        [FromHeader(Name = "Content-Type")]
        public string ContentType { get; set; }
    }
}
