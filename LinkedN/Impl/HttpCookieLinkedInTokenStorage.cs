using System;
using System.Security.Principal;
using System.Web;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for implementing a required client interface using HTTP cookies.
    /// </summary>
    public class HttpCookieLinkedInTokenStorage : IStoreLinkedInTokens
    {
        private readonly HttpContextBase _httpContextBase;

        public HttpCookieLinkedInTokenStorage(HttpContextBase httpContextBase)
        {
            _httpContextBase = httpContextBase;
            CookieExpirationUtc = DateTime.UtcNow.AddDays(30);
            CookieName = "LinkedInRestV1OAuthToken";
            CookiePath = "/";
            SlidingExpiration = true;
        }

        public DateTime CookieExpirationUtc { get; set; }
        public string CookieName { get; set; }
        public string CookiePath { get; set; }
        public bool SlidingExpiration { get; set; }

        public void Create(IPrincipal principal, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                Expire(null);
                return;
            }

            var cookie = new HttpCookie(CookieName, token)
            {
                Expires = CookieExpirationUtc,
                Path = CookiePath,
            };

            _httpContextBase.Response.SetCookie(cookie);
        }

        public void Expire(IPrincipal principal)
        {
            var cookie = new HttpCookie(CookieName, null)
            {
                Expires = DateTime.UtcNow.AddDays(-2),
            };

            _httpContextBase.Response.SetCookie(cookie);
        }

        public string Get(IPrincipal principal)
        {
            // look for the cookie in the request
            string value = null;
            var cookie = _httpContextBase.Request.Cookies[CookieName]
                         ?? _httpContextBase.Response.Cookies[CookieName];
            if (cookie != null) value = cookie.Value;
            if (SlidingExpiration && !string.IsNullOrWhiteSpace(value))
            {
                _httpContextBase.Response.SetCookie(
                    new HttpCookie(CookieName, value)
                    {
                        Expires = CookieExpirationUtc,
                        Path = "/",
                    }
                );
            }

            return value;
        }
    }
}
