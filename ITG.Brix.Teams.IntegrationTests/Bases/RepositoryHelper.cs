using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.Teams.Infrastructure.DataAccess.Repositories;
using System;

namespace ITG.Brix.Teams.IntegrationTests.Bases
{
    public static class RepositoryHelper
    {
        public static Operator CreateOperator(Guid id, string login, string firstName, string lastName)
        {
            // prepare
            var writeRepository = new OperatorWriteRepository(new PersistenceContext(new PersistenceConfiguration(DatabaseHelper.ConnectionString)));
            var readRepository = new OperatorReadRepository(new PersistenceContext(new PersistenceConfiguration(DatabaseHelper.ConnectionString)));

            // create
            var entity = new Operator(id, login, firstName, lastName);

            writeRepository.CreateAsync(entity).GetAwaiter().GetResult();

            // result
            var result = readRepository.GetAsync(id).Result;

            return result;
        }
    }
}
