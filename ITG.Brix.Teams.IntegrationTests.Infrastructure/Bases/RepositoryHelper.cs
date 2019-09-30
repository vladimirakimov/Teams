using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.Teams.Infrastructure.DataAccess.Repositories;
using ITG.Brix.Teams.Infrastructure.Providers.Impl;
using System;
using System.Collections.Generic;

namespace ITG.Brix.Teams.IntegrationTests.Infrastructure.Bases
{
    public static class RepositoryHelper
    {
        public static class ForTeam
        {
            public static Team CreateTeam(TeamId teamId, Name name)
            {
                // prepare
                var odataProvider = new TeamOdataProvider();
                var writeRepository = new TeamWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new TeamReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider);

                // create
                var team = new Team(teamId, name);

                writeRepository.CreateAsync(team).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(teamId.GetGuid()).Result;

                return result;
            }

            public static Team CreateTeamWithMembers(TeamId teamId, Name name, List<Guid> members)
            {
                // prepare
                var odataProvider = new TeamOdataProvider();
                var writeRepository = new TeamWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new TeamReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider);

                // create
                var team = new Team(teamId, name);
                foreach (var member in members)
                {
                    team.AddMember(new Member(member));
                }

                writeRepository.CreateAsync(team).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(teamId.GetGuid()).Result;

                return result;
            }

            public static IEnumerable<Team> GetTeams()
            {
                var odataProvider = new TeamOdataProvider();
                var repository = new TeamReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider);
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForOperator
        {
            public static Operator CreateOperator(Guid id, string login, string firstName, string lastName)
            {
                // prepare
                var writeRepository = new OperatorWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new OperatorReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var entity = new Operator(id, login, firstName, lastName);

                writeRepository.CreateAsync(entity).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Operator> GetOperators()
            {
                var repository = new OperatorReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }
    }
}
