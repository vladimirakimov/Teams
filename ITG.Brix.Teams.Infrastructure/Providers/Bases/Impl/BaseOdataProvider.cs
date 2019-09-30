using ITG.Brix.Teams.Infrastructure.Internal;
using StringToExpression.LanguageDefinitions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ITG.Brix.Teams.Infrastructure.Providers.Impl
{
    public class BaseOdataProvider<TEntity> : IBaseOdataProvider<TEntity>
    {
        public string FilterTransform(string filter, IDictionary<string, string> replacements)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                result = filter;
                foreach (var replacement in replacements)
                {
                    result = result.Replace(replacement.Key, replacement.Value);
                }
            }
            return result;
        }

        public Expression<Func<TEntity, bool>> GetFilterPredicate(string filter)
        {
            Expression<Func<TEntity, bool>> result = null;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var language = new ODataFilterLanguage();
                try
                {
                    result = language.Parse<TEntity>(filter);
                }
                catch (Exception exception)
                {
                    throw Error.FilterOData(exception);
                }
            }

            return result;
        }
    }
}
