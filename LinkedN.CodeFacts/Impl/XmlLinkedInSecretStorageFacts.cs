using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace LinkedN
{
    public static class XmlLinkedInSecretStorageFacts
    {
        [TestClass]
        public class TheVirtualPathProperty
        {
            [TestMethod]
            public void IsPublic()
            {
                const string value = "~/path/to/file.xml";
                var impl = new XmlLinkedInSecretStorage(null)
                {
                    VirtualPath = value,
                };
                impl.VirtualPath.ShouldEqual(value);
            }
        }
    }
}
