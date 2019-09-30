using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Constants;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations;
using ITG.Brix.Teams.Infrastructure.Internal;
using ITG.Brix.Teams.Infrastructure.Providers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.Repositories
{
    public class TeamReadRepository : ITeamReadRepository
    {
        private readonly ITeamOdataProvider _teamOdataProvider;

        private readonly IMongoCollection<TeamClass> _collection;

        public TeamReadRepository(IPersistenceContext persistenceContext,
                                  ITeamOdataProvider teamOdataProvider)
        {
            if (persistenceContext == null)
            {
                throw Error.ArgumentNull(nameof(persistenceContext));
            }
            _teamOdataProvider = teamOdataProvider ?? throw Error.ArgumentNull(nameof(teamOdataProvider));

            _collection = persistenceContext.Database.GetCollection<TeamClass>(Consts.Collections.Teams);
        }

        public async Task<IEnumerable<Team>> ListAsync(string filter, int? skip, int? limit)
        {
            var filterDefinition = _teamOdataProvider.GetFilterDefinition(filter);

            IFindFluent<TeamClass, TeamClass> fluent = _collection.Find(filterDefinition);

            fluent = fluent.Skip(skip).Limit(limit);

            var teamClasses = await fluent.ToListAsync();

            var result = new List<Team>();
            foreach (var teamClass in teamClasses)
            {
                var team = new Team(TeamId.With(teamClass.Id), new Name(teamClass.Name));
                team.SetImage(teamClass.Image);
                team.SetDescription((Description)teamClass.Description);
                team.SetImage(teamClass.Image);
                Layout layout = null;
                if (teamClass.Layout != null)
                {
                    layout = new Layout(teamClass.Layout.Id);
                }
                team.SetLayout(layout);
                team.SetFilterContent(teamClass.FilterContent);
                foreach (var member in teamClass.Members)
                {
                    team.AddMember(new Member(member.Id));
                }
                team.SetVersion(teamClass.Version);

                result.Add(team);
            }

            return result;
        }

        public async Task<Team> GetAsync(Guid id)
        {
            try
            {
                var findById = await _collection.FindAsync(doc => doc.Id == id);
                var teamClass = findById.FirstOrDefault();
                if (teamClass == null)
                {
                    throw Error.EntityNotFoundDb();
                }

                var teamId = TeamId.With(teamClass.Id);
                var team = new Team(teamId, new Name(teamClass.Name));
                team.SetImage(teamClass.Image);
                team.SetDescription((Description)teamClass.Description);
                team.SetImage(teamClass.Image);
                Layout layout = null;
                if (teamClass.Layout != null)
                {
                    layout = new Layout(teamClass.Layout.Id);
                }
                team.SetLayout(layout);
                team.SetFilterContent(teamClass.FilterContent);
                foreach (var member in teamClass.Members)
                {
                    team.AddMember(new Member(member.Id));
                }
                team.SetVersion(teamClass.Version);

                return team;
            }
            catch (MongoCommandException ex)
            {
                Debug.WriteLine(ex);
                throw Error.GenericDb(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
