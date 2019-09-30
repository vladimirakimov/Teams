using FluentAssertions;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.IntegrationTests.Infrastructure.ClassMaps
{
    [TestClass]
    public class ClassMapsRegistratorTests
    {
        [TestMethod]
        public void RegisterMapsShouldSucceed()
        {
            Exception exception = null;
            try
            {
                ClassMapsRegistrator.RegisterMaps();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().BeNull();
        }
    }
}
