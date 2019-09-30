﻿using ITG.Brix.Teams.Domain.Bases;
using ITG.Brix.Teams.Domain.Internal;
using System.Collections.Generic;

namespace ITG.Brix.Teams.Domain
{
    public class TypePlanning : ValueObject<TypePlanning>
    {
        public string Name { get; private set; }

        public TypePlanning(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.Argument(string.Format("{0} can't be empty.", nameof(name)));
            }
            Name = name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
        }
    }
}
