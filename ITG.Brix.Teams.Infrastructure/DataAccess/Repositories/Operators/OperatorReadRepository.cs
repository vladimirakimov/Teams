using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Constants;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations;
using ITG.Brix.Teams.Infrastructure.Internal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.Repositories
{
    public class OperatorReadRepository : IOperatorReadRepository
    {
        private IMongoCollection<Operator> _collection;

        public OperatorReadRepository(IPersistenceContext persistenceContext)
        {
            if (persistenceContext == null)
            {
                throw Error.ArgumentNull(nameof(persistenceContext));
            }

            _collection = persistenceContext.Database.GetCollection<Operator>(Consts.Collections.Operators);
        }


        public async Task<IEnumerable<Operator>> ListAsync(Expression<Func<Operator, bool>> filter, int? skip, int? limit)
        {
            IFindFluent<Operator, Operator> fluent = null;
            if (filter == null)
            {
                var emptyFilter = Builders<Operator>.Filter.Empty;
                fluent = _collection.Find(emptyFilter);
            }
            else
            {
                fluent = _collection.Find(filter);
            }

            fluent = fluent.Skip(skip).Limit(limit);

            return await fluent.ToListAsync();
        }

        public async Task<Operator> GetAsync(Guid id)
        {
            try
            {
                var result = await _collection.FindAsync(x => x.Id == id);
                var item = result.FirstOrDefault();

                if (item == null)
                {
                    throw Error.EntityNotFoundDb();
                }
                return item;
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
