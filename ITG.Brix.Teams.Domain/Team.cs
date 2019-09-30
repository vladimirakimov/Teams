using ITG.Brix.Teams.Domain.Bases;
using ITG.Brix.Teams.Domain.Internal;
using System.Collections.Generic;

namespace ITG.Brix.Teams.Domain
{
    public class Team : Entity<TeamId>, IAggregateRoot
    {
        private readonly MemberCollection _members;

        [Signature]
        public Name Name { get; private set; }
        public string Image { get; private set; }
        public Description Description { get; private set; }
        public DriverWait DriverWait { get; private set; }
        public Layout Layout { get; private set; }
        public string FilterContent { get; private set; }

        public IReadOnlyCollection<Member> Members => _members.AsReadOnly();

        public Team(TeamId id, Name name) : base(id)
        {
            if (name == null)
            {
                throw Error.NameShouldNotBeNull();
            }

            Name = name;
            DriverWait = DriverWait.Unspecified;
            _members = new MemberCollection();
        }

        public void SetVersion(int version)
        {
            Version = version;
        }

        public void ChangeName(Name name)
        {
            Name = name;
        }

        public void SetImage(string image)
        {
            if (string.IsNullOrWhiteSpace(image))
            {
                image = null;
            }
            Image = image;
        }

        public void SetDescription(Description description)
        {
            Description = description;
        }

        public void ChangeDriverWait(DriverWait driverWait)
        {
            DriverWait = driverWait;
        }

        public void SetLayout(Layout layout)
        {
            Layout = layout;
        }

        public void SetFilterContent(string filterContent)
        {
            FilterContent = filterContent;
        }

        public void AddMember(Member member)
        {
            _members.Add(member);
        }

        public void RemoveMember(Member member)
        {
            _members.Remove(member);
        }

        public void ClearMembers()
        {
            _members.Clear();
        }
    }
}
