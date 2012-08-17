using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace LinkedN
{
    public static class InterfaceStoreLinkedInTokensFacts
    {
        [TestClass]
        public class TheExpireMethod
        {
            [TestMethod]
            public void IsPublic()
            {
                var impl = new Mock<IStoreLinkedInTokens>(MockBehavior.Strict);
                impl.Setup(i => i.Expire(It.IsAny<IPrincipal>()));
                impl.Object.Expire(null);
                impl.Verify(i => i.Expire(It.IsAny<IPrincipal>()), Times.Once());
            }
        }

        [TestClass]
        public class TheIPrincipalParameters
        {
            [TestMethod]
            public void CanBeUsed()
            {
                var impl = new TestTokenStorage();
                impl.ShouldNotBeNull();
            }

            private class TestTokenStorage : IStoreLinkedInTokens
            {
                private readonly IDictionary<IPrincipal, string> _store = new Dictionary<IPrincipal, string>();

                public void Create(IPrincipal principal, string token)
                {
                    _store[principal] = token;
                }

                public void Expire(IPrincipal principal)
                {
                    _store.Remove(principal);
                }

                public string Get(IPrincipal principal)
                {
                    return _store[principal];
                }
            }
        }
    }
}
