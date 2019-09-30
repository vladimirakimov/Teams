﻿using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Constants;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations;
using ITG.Brix.Teams.Infrastructure.Extensions;
using ITG.Brix.Teams.Infrastructure.Internal;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.Repositories
{
    public class OperatorWriteRepository : IOperatorWriteRepository
    {
        private readonly IMongoCollection<Operator> _collection;

        public OperatorWriteRepository(IPersistenceContext persistenceContext)
        {
            if (persistenceContext == null)
            {
                throw Error.ArgumentNull(nameof(persistenceContext));
            }

            _collection = persistenceContext.Database.GetCollection<Operator>(Consts.Collections.Operators);
        }

        public async Task CreateAsync(Operator entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
            }
            catch (MongoWriteException ex)
            {
                if (ex.IsUniqueViolation())
                {
                    throw Error.UniqueKey(ex);
                }
                throw Error.GenericDb(ex);
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

        public async Task UpdateAsync(Operator entity)
        {
            try
            {
                await _collection.ReplaceOneAsync(doc => doc.Id == entity.Id, entity);
            }
            catch (MongoWriteException ex)
            {
                if (ex.IsUniqueViolation())
                {
                    throw Error.UniqueKey(ex);
                }
                throw Error.GenericDb(ex);
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

        public async Task DeleteAsync(Guid id, int version)
        {
            try
            {
                var findById = await _collection.FindAsync(doc => doc.Id == id);
                var user = findById.FirstOrDefault();
                if (user == null)
                {
                    throw Error.EntityNotFoundDb();
                }

                var result = await _collection.DeleteOneAsync(doc => doc.Id == id && doc.Version == version);
                if (result.DeletedCount == 0)
                {
                    throw Error.EntityVersionDb();
                }
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
