using System;
using System.Collections.Generic;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models.From
{
    public class UpdateTeamFromBody
    {
        public string Put { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string DriverWait { get; set; }
        public string Layout { get; set; }
        public List<Guid> Members { get; set; }
        public string FilterContent { get; set; }
    }
}
