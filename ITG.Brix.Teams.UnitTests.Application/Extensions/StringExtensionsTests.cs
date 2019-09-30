using FluentAssertions;
using ITG.Brix.Teams.Application.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.Application.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow("", "")]
        [DataRow("    ", "    ")]
        [DataRow("a", "a")]
        [DataRow("Ab", "ab")]
        [DataRow("AB", "aB")]
        public void ToCamelCaseShouldSucceed(string input, string expected)
        {
            // Act
            var result = input.ToCamelCase();

            // Assert
            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow("", null)]
        [DataRow("    ", null)]
        [DataRow("a", null)]
        [DataRow("1", 1)]
        public void ToNullableIntShouldSucceed(string input, int? expected)
        {
            // Act
            var result = input.ToNullableInt();

            // Assert
            result.Should().Be(expected);
        }

    }
}
