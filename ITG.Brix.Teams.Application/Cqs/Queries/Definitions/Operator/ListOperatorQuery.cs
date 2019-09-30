using ITG.Brix.Teams.Application.Bases;
using MediatR;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Definitions
{
    public class ListOperatorQuery : IRequest<Result>
    {
        public string Filter { get; private set; }
        public string Top { get; private set; }
        public string Skip { get; private set; }

        public ListOperatorQuery(string filter, string top, string skip)
        {
            Filter = filter;
            Top = top;
            Skip = skip;
        }
    }
}
