using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace LinkedN
{
    public static class HttpCookieLinkedInTokenStorageFacts
    {
        [TestClass]
        public class TheCookieExpirationUtcProperty
        {
            [TestMethod]
            public void IsPublic()
            {
                var value = DateTime.UtcNow.AddDays(15);
                var impl = new HttpCookieLinkedInTokenStorage(null)
                {
                    CookieExpirationUtc = value,
                };
                impl.CookieExpirationUtc.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TheCookieNameProperty
        {
            [TestMethod]
            public void IsPublic()
            {
                const string value = "test";
                var impl =new HttpCookieLinkedInTokenStorage(null)
                {
                    CookieName = "test",
                };
                impl.CookieName.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TheCookiePathProperty
        {
            [TestMethod]
            public void IsPublic()
            {
                const string value = "test";
                var impl = new HttpCookieLinkedInTokenStorage(null)
                {
                    CookiePath = "test",
                };
                impl.CookiePath.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TheSlidingExpirationProperty
        {
            [TestMethod]
            public void IsPublic()
            {
                const bool value = false;
                var impl = new HttpCookieLinkedInTokenStorage(null)
                {
                    SlidingExpiration = value,
                };
                impl.SlidingExpiration.ShouldEqual(value);
            }
        }
    }
}
