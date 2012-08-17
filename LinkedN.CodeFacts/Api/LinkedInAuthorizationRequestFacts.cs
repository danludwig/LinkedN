using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace LinkedN
{
    public static class LinkedInAuthorizationRequestFacts
    {
        [TestClass]
        public class TheRequestParametersProperty
        {
            [TestMethod]
            public void IsPublic()
            {
                var value = new Dictionary<string, string>();
                var instance = new LinkedInUserAuthorizationRequest(new Uri("http://www.domain.tld"))
                {
                    RequestParamaters = value,
                };
                instance.RequestParamaters.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TheRedirectParametersProperty
        {
            [TestMethod]
            public void IsPublic()
            {
                var value = new Dictionary<string, string>();
                var instance = new LinkedInUserAuthorizationRequest(new Uri("http://www.domain.tld"))
                {
                    RedirectParamaters = value,
                };
                instance.RedirectParamaters.ShouldEqual(value);
            }
        }
    }
}
