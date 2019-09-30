using ITG.Brix.Teams.Domain.Bases;

namespace ITG.Brix.Teams.Domain
{
    public class TeamId : Identity<TeamId>
    {
        public TeamId(string value) : base(value) { }
    }
}
