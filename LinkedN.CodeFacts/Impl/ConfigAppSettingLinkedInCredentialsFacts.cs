using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedN
{
    public static class ConfigAppSettingLinkedInCredentialsFacts
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void IsPublic()
            {
                // ReSharper disable ObjectCreationAsStatement
                new ConfigAppSettingLinkedInCredentials("test", "test");
                // ReSharper restore ObjectCreationAsStatement
            }
        }
    }
}
