using ITG.Brix.Teams.Domain.Bases;
using ITG.Brix.Teams.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.Teams.Domain
{
    public class Member : ValueObject<Member>
    {
        private readonly Guid _id;

        public Member(Guid id)
        {
            if (id == default(Guid))
            {
                throw Error.MemberFieldShouldNotBeDefaultGuid();
            }

            _id = id;
        }

        public static implicit operator Member(Guid id)
        {
            return id.Equals(Guid.Empty) ? null : new Member(id);
        }

        public static implicit operator Guid(Member member)
        {
            return member == null ? Guid.Empty : member._id;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _id;
        }
    }
}
