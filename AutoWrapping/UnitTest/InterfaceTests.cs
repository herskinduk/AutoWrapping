using Moq;
using NUnit.Framework;
using Sitecore.Data.Items;

namespace Cardinal.Core.Abstractions.UnitTest
{
    [TestFixture]
    public class InterfaceTests
    {
        // example unit test
        [Test]
        public void TestSomething()
        {
            // set up the mock
            Mock<IItem> mockItem = new Mock<IItem>();
            mockItem.SetupGet(x => x.HasChildren).Returns(false);
            
            // use the mock

            Item item = mockItem.Object.InnerWrappedObject;
            mockItem.Verify();


        }
    }
}
