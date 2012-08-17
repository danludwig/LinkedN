using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace LinkedN
{
    public static class InterfaceHandleLinkedInRequestFacts
    {
        [TestClass]
        public class TheRequestUsingStringMethod
        {
            [TestMethod]
            public void IsPublic()
            {
                const string inputValue = "test";
                const string outputValue = "also test";
                var impl = new Mock<IHandleLinkedInRequest<object>>(MockBehavior.Strict);
                impl.Setup(i => i.SendRequestFrom(inputValue)).Returns(outputValue);
                var result = impl.Object.SendRequestFrom(inputValue);
                result.ShouldEqual(outputValue);
            }
        }

        [TestClass]
        public class TheBuildRequestUrlMethod
        {
            [TestMethod]
            public void IsPublic()
            {
                const string outputValue = "also test";
                var impl = new Mock<IHandleLinkedInRequest<object>>(MockBehavior.Strict);
                impl.Setup(i => i.BuildRequestUrl()).Returns(outputValue);
                var result = impl.Object.BuildRequestUrl();
                result.ShouldEqual(outputValue);
            }
        }
    }
}
