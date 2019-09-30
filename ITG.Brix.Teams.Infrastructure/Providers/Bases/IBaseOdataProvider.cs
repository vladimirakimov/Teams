using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ITG.Brix.Teams.Infrastructure.Providers
{
    public interface IBaseOdataProvider<TEntity>
    {
        string FilterTransform(string filter, IDictionary<string, string> replacements);
        Expression<Func<TEntity, bool>> GetFilterPredicate(string filter);
    }
}
