using System.Collections.Generic;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Models
{
    public class OperatorsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<OperatorModel> Value { get; set; }
    }
}
