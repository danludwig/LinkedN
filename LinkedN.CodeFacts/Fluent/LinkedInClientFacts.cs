using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace LinkedN
{
    public static class LinkedInClientFacts
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void IsPublic()
            {
                var client = new LinkedInClient(null, null, null);
                client.ShouldNotBeNull();
            }
        }
    }
}
