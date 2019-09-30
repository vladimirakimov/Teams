using System;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Domain.Repositories
{
    public interface IOperatorWriteRepository
    {
        Task CreateAsync(Operator entity);
        Task UpdateAsync(Operator entity);
        Task DeleteAsync(Guid id, int version);
    }
}
