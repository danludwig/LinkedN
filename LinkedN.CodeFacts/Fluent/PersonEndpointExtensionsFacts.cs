using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace LinkedN
{
    public static class PersonRequestExtensionsFacts
    {
        [TestClass]
        public class TheMemberIdMethod
        {
            [TestMethod]
            public void IsPublic()
            {
                const string value = "test";
                var endpoint = new Mock<IHandleLinkedInRequest<Person>>(MockBehavior.Strict);
                endpoint.Setup(e => e.SetRequestOption(It.IsAny<Enum>(), It.IsAny<string>()));
                endpoint.Setup(e => e.GetRequestOption(It.IsAny<Enum>())).Returns(null as string);
                var result = endpoint.Object.MemberId(value);
                result.ShouldNotBeNull();
            }
        }

        [TestClass]
        public class TheMemberUrlMethod
        {
            [TestMethod]
            public void IsPublic()
            {
                const string value = "test";
                var endpoint = new Mock<IHandleLinkedInRequest<Person>>(MockBehavior.Strict);
                endpoint.Setup(e => e.SetRequestOption(It.IsAny<Enum>(), It.IsAny<string>()));
                endpoint.Setup(e => e.GetRequestOption(It.IsAny<Enum>())).Returns(null as string);
                var result = endpoint.Object.MemberUrl(value);
                result.ShouldNotBeNull();
            }
        }

        [TestClass]
        public class TheStandardMethod
        {
            [TestMethod]
            public void IsPublic()
            {
                var endpoint = new Mock<IHandleLinkedInRequest<Person>>(MockBehavior.Strict);
                endpoint.Setup(e => e.SetRequestOption(It.IsAny<Enum>(), It.IsAny<string>()));
                var result = endpoint.Object.Standard();
                result.ShouldNotBeNull();
            }
        }

        [TestClass]
        public class TheStandardMethodPersonFieldGroupsOverload
        {
            [TestMethod]
            public void IsPublic()
            {
                var endpoint = new Mock<IHandleLinkedInRequest<Person>>(MockBehavior.Strict);
                endpoint.Setup(e => e.SetRequestOption(It.IsAny<Enum>(), It.IsAny<string>()));
                endpoint.Setup(e => e.GetRequestOption(It.IsAny<Enum>())).Returns(null as string);
                var result = endpoint.Object.Select(PersonFieldGroup.Rrrrrvrything);
                result.ShouldNotBeNull();
            }
        }
    }
}
