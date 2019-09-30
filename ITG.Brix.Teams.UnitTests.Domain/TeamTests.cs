using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void CreateTeamShouldSucceed()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");

            // Act
            var result = new Team(id, name);

            // Assert
            result.Id.Should().Be(id);
            result.Name.Should().Be(name);
            result.DriverWait.Should().Be(DriverWait.Unspecified);
            result.Members.Should().BeEmpty();
        }

        [TestMethod]
        public void CreateTeamShouldFailWhenNameIsNull()
        {
            // Arrange
            var id = TeamId.New;
            Name name = null;

            // Act
            Action ctor = () => { new Team(id, name); };

            // Assert
            ctor.Should().Throw<NameShouldNotBeNullException>();
        }


        [TestMethod]
        public void TeamsAreEqualWhenTheyHaveSameNames()
        {
            // Arrange
            var id1 = TeamId.New;
            var id2 = TeamId.New;
            var name = new Name("name");

            var domain1 = new Team(id1, name);
            var domain2 = new Team(id2, name);

            // Act
            var result = domain1.Equals(domain2);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TeamsAreDifferentWhenTheyHaveDifferentNames()
        {
            var id = TeamId.New;
            var name1 = new Name("name-1");
            var name2 = new Name("name-2");

            var domain1 = new Team(id, name1);
            var domain2 = new Team(id, name2);

            // Act
            var result = domain1.Equals(domain2);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void ChangeNameShouldSucceed()
        {
            var id = TeamId.New;
            var name1 = new Name("name-1");
            var name2 = new Name("name-2");

            var domain = new Team(id, name1);

            // Act
            domain.ChangeName(name2);

            // Assert
            domain.Name.Should().Be(name2);
        }

        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow("", null)]
        [DataRow("   ", null)]
        [DataRow("image", "image")]
        public void SetImageShouldSucceed(string image, string expected)
        {
            var id = TeamId.New;
            var name = new Name("name");

            var domain = new Team(id, name);

            // Act
            domain.SetImage(image);

            // Assert
            domain.Image.Should().Be(expected);
        }

        [TestMethod]
        public void SetDescriptionShouldSucceed()
        {
            var id = TeamId.New;
            var name = new Name("name");
            var description = new Description("description");

            var domain = new Team(id, name);

            // Act
            domain.SetDescription(description);

            // Assert
            domain.Description.Should().Be(description);
        }

        [TestMethod]
        public void ChangeDriverWaitShouldSucceed()
        {
            var id = TeamId.New;
            var name = new Name("name");
            var driverWait = DriverWait.Yes;

            var domain = new Team(id, name);

            // Act
            domain.ChangeDriverWait(driverWait);

            // Assert
            domain.DriverWait.Should().Be(driverWait);
        }

        [TestMethod]
        public void SetLayoutShouldSucceed()
        {
            var id = TeamId.New;
            var name = new Name("name");
            var layout = new Layout(Guid.NewGuid());

            var domain = new Team(id, name);

            // Act
            domain.SetLayout(layout);

            // Assert
            domain.Layout.Should().Be(layout);
        }

        #region Members

        [TestMethod]
        public void AddMemberShouldSucceed()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");

            var entity = new Team(id, name);

            var memberId = Guid.NewGuid();
            var member = new Member(memberId);

            // Act
            entity.AddMember(member);

            // Assert
            entity.Members.Count.Should().Be(1);
            entity.Members.ElementAt(0).Should().Be(member);
        }

        [TestMethod]
        public void AddExistingMemberShouldNotModifyCollection()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");

            var entity = new Team(id, name);

            var memberId = Guid.NewGuid();
            var member = new Member(memberId);
            entity.AddMember(member);

            // Act
            entity.AddMember(member);

            // Assert
            entity.Members.Count.Should().Be(1);
            entity.Members.ElementAt(0).Should().Be(member);
        }

        [TestMethod]
        public void RemoveMemberShouldSucceed()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");

            var entity = new Team(id, name);

            var memberId = Guid.NewGuid();
            var member = new Member(memberId);
            entity.AddMember(member);

            // Act
            entity.RemoveMember(member);

            // Assert
            entity.Members.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveUnexistingMemberShouldPassSilentlyWithoutAnyImpactOnCollection()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");

            var entity = new Team(id, name);

            var memberId = Guid.NewGuid();
            var member = new Member(memberId);
            entity.AddMember(member);

            var memberToRemove = new Member(Guid.NewGuid());

            // Act
            entity.RemoveMember(memberToRemove);

            // Assert
            entity.Members.Count.Should().Be(1);
            entity.Members.ElementAt(0).Should().Be(member);
        }

        [TestMethod]
        public void ClearMembersShouldSucceed()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");

            var entity = new Team(id, name);

            var memberId = Guid.NewGuid();
            var member = new Member(memberId);
            entity.AddMember(member);

            // Act
            entity.ClearMembers();

            // Assert
            entity.Members.Count.Should().Be(0);
        }

        #endregion

    }
}

