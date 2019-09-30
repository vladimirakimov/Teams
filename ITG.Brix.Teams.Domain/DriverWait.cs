using ITG.Brix.Teams.Domain.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.Teams.Domain
{
    public class DriverWait : IComparable
    {
        public static readonly DriverWait Unspecified = new DriverWait(1, "Unspecified");
        public static readonly DriverWait Yes = new DriverWait(2, "Yes");
        public static readonly DriverWait No = new DriverWait(3, "No");

        public string Name { get; private set; }

        public int Id { get; private set; }

        protected DriverWait() { }

        public DriverWait(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<DriverWait> List()
        {
            return new[] { Yes, No, Unspecified };
        }

        public static DriverWait Parse(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw Error.InvalidEnumeration("Invalid enumeration: {0}. Allowed values: {1}", name, string.Join(", ", List().Select(s => s.Name)));
            }

            return state;
        }

        public int CompareTo(object obj) => Id.CompareTo(((DriverWait)obj).Id);

        public override bool Equals(object obj)
        {
            var otherValue = obj as DriverWait;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
