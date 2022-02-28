using NUnit.Framework;
using SimpleCRM.Tests.Application.Helpers;

namespace SimpleCRM.Tests.Application
{
    public class TemplateTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldLoadTemplateFromResources()
        {
            // Arrange
            var templateBytes = ResourcesHelper.GetTemplateBytes();

            // Act
            var isEmpty = templateBytes.Length == 0;

            // Assert
            Assert.IsFalse(isEmpty);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}