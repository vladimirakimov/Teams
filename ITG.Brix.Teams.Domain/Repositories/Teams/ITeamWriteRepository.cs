using System;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Domain.Repositories
{
    public interface ITeamWriteRepository
    {
        Task CreateAsync(Team team);
        Task UpdateAsync(Team team);
        Task DeleteAsync(Guid id, int version);
    }
}
