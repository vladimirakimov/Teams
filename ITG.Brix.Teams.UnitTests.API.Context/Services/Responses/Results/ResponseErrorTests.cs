using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Responses.Models.Errors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Responses.Results
{
    [TestClass]
    public class ResponseErrorTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange 
            List<ResponseErrorField> details = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };

            // Act
            var obj = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details } };
            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void EqualsShouldSucceed()
        {
            // Arrange
            List<ResponseErrorField> details1 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };
            List<ResponseErrorField> details2 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };

            // Act
            var obj1 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details1 } };
            var obj2 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details2 } };
            // Assert
            obj1.Should().Be(obj2);
        }

        [TestMethod]
        public void EqualsShouldFailWhenResponseErrorBodyIsNullIsNull()
        {
            // Arrange
            List<ResponseErrorField> details1 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };

            // Act
            var obj1 = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details1 };
            var result = obj1.Equals(null);
            // Assert
            result.Should().BeFalse();
        }
        [TestMethod]
        public void EqualsShouldFailWhenOtherIsNull()
        {
            // Arrange
            List<ResponseErrorField> details1 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };

            // Act
            var obj1 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details1 } };
            var result = obj1.Equals(null);
            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void EqualsShouldFailWhenOtherCodeIsNull()
        {
            // Arrange
            List<ResponseErrorField> details1 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };
            List<ResponseErrorField> details2 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };

            // Act
            var obj1 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details1 } };
            var obj2 = new ResponseError() { Error = new ResponseErrorBody { Code = null, Message = "bodyMessage", Details = details2 } };
            var result = obj1.Equals(obj2);
            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void EqualsShouldFailWhenOtherMessageIsNull()
        {
            // Arrange
            List<ResponseErrorField> details1 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };
            List<ResponseErrorField> details2 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };

            // Act
            var obj1 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details1 } };
            var obj2 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = null, Details = details2 } };
            var result = obj1.Equals(obj2);
            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void EqualsShouldSuccessWhenBothListsAreNull()
        {
            // Arrange
            var obj1 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = null } };
            var obj2 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = null } };
            // Act

            var result = obj1.Equals(obj2);
            // Assert
            result.Should().BeTrue();
        }
        [TestMethod]
        public void EqualsShouldFailWhenOtherListIsNull()
        {
            // Arrange
            List<ResponseErrorField> details1 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };

            // Act
            var obj1 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details1 } };
            var obj2 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = null, Details = null } };
            var result = obj1.Equals(obj2);
            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void EqualsShouldFailWhenOriginListIsNull()
        {
            // Arrange
            List<ResponseErrorField> details2 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };

            // Act
            var obj1 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = null } };
            var obj2 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details2 } };
            var result = obj1.Equals(obj2);
            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void EqualsShouldFailWhenListsHaveDifferentCount()
        {
            // Arrange
            List<ResponseErrorField> details1 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" }, new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };
            List<ResponseErrorField> details2 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };

            // Act
            var obj1 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details1 } };
            var obj2 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details2 } };
            var result = obj1.Equals(obj2);
            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void GetHashCodeReturnsInt()
        {
            // Arrange

            // Act
            List<ResponseErrorField> details1 = new List<ResponseErrorField>() { new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" }, new ResponseErrorField { Code = "fieldCode", Message = "fieldMesssage", Target = "fieldTarget" } };
            var obj1 = new ResponseError() { Error = new ResponseErrorBody { Code = "bodyCode", Message = "bodyMessage", Details = details1 } };
            var result = obj1.GetHashCode();
            // Assert
            result.Should().BeOfType(typeof(int));
        }
    }
}
