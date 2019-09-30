using ITG.Brix.Teams.Domain.Bases;
using ITG.Brix.Teams.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.Teams.Domain
{
    public class Layout : ValueObject<Layout>
    {
        private readonly Guid _id;

        public Layout(Guid id)
        {
            if (id == default(Guid))
            {
                throw Error.LayoutFieldShouldNotBeDefaultGuid();
            }

            _id = id;
        }

        public static implicit operator Layout(Guid id)
        {
            return id.Equals(Guid.Empty) ? null : new Layout(id);
        }

        public static implicit operator Guid(Layout layout)
        {
            return layout == null ? Guid.Empty : layout._id;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _id;
        }
    }
}
