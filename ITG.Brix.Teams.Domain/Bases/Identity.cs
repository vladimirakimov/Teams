using ITG.Brix.Teams.Domain.Extensions;
using ITG.Brix.Teams.Domain.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ITG.Brix.Teams.Domain.Bases
{
    public abstract class Identity<T> : SingleValueObject<string>, IIdentity
        where T : Identity<T>
    {
        private readonly Lazy<Guid> _lazyGuid;

        protected Identity(string value) : base(value)
        {
            var consistencyFailures = new List<string>(Validate(value));
            if (consistencyFailures.Any())
            {
                var message = string.Join(", ", consistencyFailures);
                throw Error.InvalidIdentity("Invalid identity: {0}", message);
            }

            _lazyGuid = new Lazy<Guid>(() => Guid.Parse(Value));
        }

        public static T New => With(Guid.NewGuid());

        public static T With(string value)
        {
            try
            {
                return (T)Activator.CreateInstance(typeof(T), value);
            }
            catch (TargetInvocationException exception)
            {
                if (exception.InnerException != null)
                {
                    throw exception.InnerException;
                }
                throw;
            }
        }

        public static T With(Guid guid)
        {
            var value = guid.ToString();
            return With(value);
        }

        public Guid GetGuid() => _lazyGuid.Value;

        public static bool IsValid(string value)
        {
            var consistencyFailures = Validate(value);
            var anyFailure = consistencyFailures.Any();
            return !anyFailure;
        }

        private static IEnumerable<string> Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                yield return string.Format("Identity of type '{0}' cannot be empty.", typeof(T).ToPrettyString());
                yield break;
            }

            if (!string.Equals(value.Trim(), value, StringComparison.OrdinalIgnoreCase))
            {
                yield return string.Format("Identity '{0}' of type '{1}' contains leading and/or trailing spaces", value, typeof(T).ToPrettyString());
                yield break;
            }

            if (!Guid.TryParse(value, out Guid guid))
            {
                yield return string.Format("Identity '{0}' has invalid value.", value);
                yield break;
            }
            else if (guid.Equals(Guid.Empty))
            {
                yield return string.Format("Identity '{0}' not allowed.", value);
            }
        }

    }
}
