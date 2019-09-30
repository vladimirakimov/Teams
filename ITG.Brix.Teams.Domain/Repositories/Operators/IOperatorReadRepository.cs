using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Domain.Repositories
{
    public interface IOperatorReadRepository
    {
        Task<IEnumerable<Operator>> ListAsync(Expression<Func<Operator, bool>> filter, int? skip, int? limit);
        Task<Operator> GetAsync(Guid id);
    }
}
