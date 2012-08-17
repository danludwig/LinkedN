using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace LinkedN
{
    public static class DefaultLinkedInClientFacts
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void IsPublic()
            {
                HttpContext.Current = new HttpContext(
                    new HttpRequest(null, "http://www.domain.tld", null), 
                    new HttpResponse(null)
                );
                var impl = new DefaultLinkedInClient();
                impl.ShouldNotBeNull();
            }
        }
    }
}
