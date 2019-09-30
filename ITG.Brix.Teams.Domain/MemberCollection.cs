using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ITG.Brix.Teams.Domain
{
    public sealed class MemberCollection
    {
        private readonly IList<Member> _members;

        public MemberCollection()
        {
            _members = new List<Member>();
        }

        public IReadOnlyCollection<Member> AsReadOnly()
        {
            IReadOnlyCollection<Member> result = new ReadOnlyCollection<Member>(_members);
            return result;
        }

        public void Add(Member member)
        {
            if (!_members.Contains(member))
            {
                _members.Add(member);
            }
        }

        public void Remove(Member member)
        {
            if (_members.Contains(member))
            {
                _members.Remove(member);
            }
        }

        public void Clear()
        {
            _members.Clear();
        }
    }
}
