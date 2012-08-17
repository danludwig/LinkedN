using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace LinkedN
{
    public static class PersonRequestHandlerFacts
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void IsPublic()
            {
                var impl = new PersonRequestHandler(null, null);
                impl.ShouldNotBeNull();
            }
        }
    }
}
