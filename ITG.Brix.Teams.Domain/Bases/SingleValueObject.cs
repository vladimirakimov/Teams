using ITG.Brix.Teams.Domain.Extensions;
using ITG.Brix.Teams.Domain.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ITG.Brix.Teams.Domain.Bases
{
    public abstract class SingleValueObject<T> : ValueObject<SingleValueObject<T>>, IComparable, ISingleValueObject
        where T : IComparable
    {
        private static readonly Type Type = typeof(T);
        private static readonly TypeInfo TypeInfo = typeof(T).GetTypeInfo();

        public T Value { get; }

        protected SingleValueObject(T value)
        {
            if (TypeInfo.IsEnum && !Enum.IsDefined(Type, value))
            {
                throw Error.Argument(string.Format("{0} isn't defined in enum '{1}'", value, Type.ToPrettyString()));
            }

            Value = value;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var other = obj as SingleValueObject<T>;
            if (other == null)
            {
                throw Error.Argument(string.Format("Cannot compare '{0}' and '{1}'", GetType().ToPrettyString(), obj.GetType().ToPrettyString()));
            }

            return Value.CompareTo(other.Value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString()
        {
            var result = ReferenceEquals(Value, null) ? string.Empty : Value.ToString();
            return result;
        }

        public object GetValue()
        {
            return Value;
        }
    }
}
