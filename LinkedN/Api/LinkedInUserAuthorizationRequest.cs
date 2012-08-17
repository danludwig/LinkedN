using System;
using System.Collections.Generic;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for encapsulating available parameters when sending an OAuth user authorization request to Linkedin.
    /// </summary>
    public class LinkedInUserAuthorizationRequest
    {
        public LinkedInUserAuthorizationRequest(Uri callback)
        {
            if (callback == null) throw new ArgumentNullException("callback");
            Callback = callback;
        }

        public LinkedInUserAuthorizationRequest(Uri baseCallback, string authorityRelativeCallback)
        {
            if (baseCallback == null) throw new ArgumentNullException("baseCallback");
            var callback = new Uri(string.Format("{0}://{1}{2}", baseCallback.Scheme, baseCallback.Authority, authorityRelativeCallback));
            Callback = callback;
        }

        public Uri Callback { get; private set; }

        public IDictionary<string, string> RequestParamaters { get; set; }

        public IDictionary<string, string> RedirectParamaters { get; set; }
    }
}
