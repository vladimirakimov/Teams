using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class LayoutTests
    {
        [TestMethod]
        public void CreateLayoutShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = new Layout(id);

            // Assert
            result.Should().Be(new Layout(id));
        }

        [TestMethod]
        public void CreateLayoutShouldFailWhenGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;

            // Act
            Action ctor = () => { new Layout(id); };

            // Assert
            ctor.Should().Throw<LayoutFieldShouldNotBeDefaultGuidException>();
        }

        [TestMethod]
        public void OperatorToGuidShouldReturnCorrectValue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var layout = new Layout(id);

            // Act
            Guid result = layout;

            // Assert
            result.Should().Be(id);
        }

        [TestMethod]
        public void OperatorToGuidShouldReturnEmptyGuid()
        {
            // Arrange
            Layout layout = null;

            // Act
            Guid result = layout;

            // Assert
            result.Should().Be(Guid.Empty);
        }

        [TestMethod]
        public void OperatorToLayoutShouldReturnCorrectValue()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = (Layout)id;

            // Assert
            result.Should().Be(new Layout(id));
        }

        [TestMethod]
        public void LayoutsAreEqualWhenTheyHaveSameIds()
        {
            // Arrange
            var id = Guid.NewGuid();

            var domain1 = new Layout(id);
            var domain2 = new Layout(id);

            // Act
            var result = domain1.Equals(domain2);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void LayoutsAreDifferentWhenTheyHaveDifferentIds()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var domain1 = new Layout(id1);
            var domain2 = new Layout(id2);

            // Act
            var result = domain1.Equals(domain2);

            // Assert
            result.Should().BeFalse();
        }
    }
}

