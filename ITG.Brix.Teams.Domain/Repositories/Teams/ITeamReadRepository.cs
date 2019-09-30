using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Domain.Repositories
{
    public interface ITeamReadRepository
    {
        Task<IEnumerable<Team>> ListAsync(string filter, int? skip, int? limit);
        Task<Team> GetAsync(Guid id);
    }
}
