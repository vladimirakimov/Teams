using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.Teams.Infrastructure.Internal;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ITG.Brix.Teams.Infrastructure.Providers.Impl
{
    public class TeamOdataProvider : BaseOdataProvider<TeamClass>, ITeamOdataProvider
    {
        public FilterDefinition<TeamClass> GetFilterDefinition(string filter)
        {
            FilterDefinition<TeamClass> filterDefinition = Builders<TeamClass>.Filter.Empty;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                if (filter.StartsWith("members/id", StringComparison.InvariantCultureIgnoreCase))
                {
                    var match = Regex.Match(filter, @"([a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12})");
                    if (match.Success)
                    {
                        var guid = Guid.Parse(match.Value);
                        filterDefinition = Builders<TeamClass>.Filter.ElemMatch(foo => foo.Members, r => r.Id == guid);
                    }
                    else
                    {
                        throw Error.FilterOData("Filter by member incorrect format");
                    }
                }
                else
                {
                    Expression<Func<TeamClass, bool>> filterPredicate = GetFilterPredicate(filter);
                    filterDefinition = filterPredicate;
                }
            }

            return filterDefinition;
        }

    }
}
