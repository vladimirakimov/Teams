using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Constants;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations;
using ITG.Brix.Teams.Infrastructure.Extensions;
using ITG.Brix.Teams.Infrastructure.Internal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.Repositories
{
    public class TeamWriteRepository : ITeamWriteRepository
    {
        private readonly IMongoCollection<TeamClass> _collection;

        public TeamWriteRepository(IPersistenceContext persistenceContext)
        {
            if (persistenceContext == null)
            {
                throw Error.ArgumentNull(nameof(persistenceContext));
            }

            _collection = persistenceContext.Database.GetCollection<TeamClass>(Consts.Collections.Teams);
        }

        public async Task CreateAsync(Team team)
        {
            try
            {
                var members = new List<MemberClass>();
                foreach (var member in team.Members)
                {
                    var memberClass = new MemberClass() { Id = member };
                    members.Add(memberClass);
                }
                var teamClass = new TeamClass()
                {
                    Id = team.Id.GetGuid(),
                    Version = team.Version,
                    Name = team.Name,
                    Image = team.Image,
                    Description = team.Description,
                    Layout = team.Layout == null ? null : new LayoutClass() { Id = team.Layout },
                    FilterContent = team.FilterContent,
                    Members = members
                };

                await _collection.InsertOneAsync(teamClass);
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

        public async Task UpdateAsync(Team team)
        {
            try
            {
                var members = new List<MemberClass>();
                foreach (var member in team.Members)
                {
                    var memberClass = new MemberClass() { Id = member };
                    members.Add(memberClass);
                }
                var teamClass = new TeamClass()
                {
                    Id = team.Id.GetGuid(),
                    Version = team.Version,
                    Name = team.Name,
                    Image = team.Image,
                    Description = team.Description,
                    Layout = team.Layout != null ? new LayoutClass() { Id = team.Layout } : null,
                    FilterContent = team.FilterContent,
                    Members = members
                };

                await _collection.ReplaceOneAsync(doc => doc.Id == teamClass.Id, teamClass);
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
