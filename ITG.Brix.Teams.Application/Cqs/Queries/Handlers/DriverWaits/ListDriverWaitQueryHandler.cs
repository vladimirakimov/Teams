using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using ITG.Brix.Teams.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Handlers
{
    public class ListDriverWaitQueryHandler : IRequestHandler<ListDriverWaitQuery, Result>
    {
        public async Task<Result> Handle(ListDriverWaitQuery query, CancellationToken cancellationToken)
        {
            var result = DriverWait.List().Select(x => x.Name);

            return await Task.FromResult(Result.Ok(result));
        }
    }
}
