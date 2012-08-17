using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace LinkedN
{
    public static class ResourceModelFacts
    {
        [TestClass]
        public class TheUriProperty
        {
            [TestMethod]
            public void IsPublic()
            {
                var impl = new Mock<ResourceModel>(MockBehavior.Strict);
                impl.Object.Uri.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheResourceProperty
        {
            [TestMethod]
            public void IsPublic()
            {
                var impl = new Mock<ResourceModel>(MockBehavior.Strict);
                impl.Object.Resource.ShouldBeNull();
            }
        }
    }
}
