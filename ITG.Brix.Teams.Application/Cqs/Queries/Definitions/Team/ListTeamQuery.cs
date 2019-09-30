using ITG.Brix.Teams.Application.Bases;
using MediatR;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Definitions
{
    public class ListTeamQuery : IRequest<Result>
    {
        public string Filter { get; private set; }
        public string Top { get; private set; }
        public string Skip { get; private set; }

        public ListTeamQuery(string filter, string top, string skip)
        {
            Filter = filter;
            Top = top;
            Skip = skip;
        }
    }
}
