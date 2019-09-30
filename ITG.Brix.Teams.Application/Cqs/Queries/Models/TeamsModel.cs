using System.Collections.Generic;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Models
{
    public class TeamsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<TeamModel> Value { get; set; }
    }
}
