using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class MemberTests
    {
        [TestMethod]
        public void CreateMemberShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = new Member(id);

            // Assert
            result.Should().Be(new Member(id));
        }

        [TestMethod]
        public void CreateMemberShouldFailWhenGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;

            // Act
            Action ctor = () => { new Member(id); };

            // Assert
            ctor.Should().Throw<MemberFieldShouldNotBeDefaultGuidException>();
        }

        [TestMethod]
        public void OperatorToGuidShouldReturnCorrectValue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var member = new Member(id);

            // Act
            Guid result = member;

            // Assert
            result.Should().Be(id);
        }

        [TestMethod]
        public void OperatorToGuidShouldReturnEmptyGuid()
        {
            // Arrange
            Member member = null;

            // Act
            Guid result = member;

            // Assert
            result.Should().Be(Guid.Empty);
        }

        [TestMethod]
        public void OperatorToMemberShouldReturnCorrectValue()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = (Member)id;

            // Assert
            result.Should().Be(new Member(id));
        }

        [TestMethod]
        public void MembersAreEqualWhenTheyHaveSameIds()
        {
            // Arrange
            var id = Guid.NewGuid();

            var domain1 = new Member(id);
            var domain2 = new Member(id);

            // Act
            var result = domain1.Equals(domain2);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void MembersAreDifferentWhenTheyHaveDifferentIds()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var domain1 = new Member(id1);
            var domain2 = new Member(id2);

            // Act
            var result = domain1.Equals(domain2);

            // Assert
            result.Should().BeFalse();
        }
    }
}

