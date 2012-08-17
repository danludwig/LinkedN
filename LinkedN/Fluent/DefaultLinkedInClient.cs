using System.Web;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for providing an out-of-the-box Linkedin client that can be constructed without any arguments.
    /// </summary>
    public class DefaultLinkedInClient : LinkedInClient
    {
        public DefaultLinkedInClient()
            : base(null, // yuck
            new HttpCookieLinkedInTokenStorage(new HttpContextWrapper(HttpContext.Current)), 
            new XmlLinkedInSecretStorage(new DefaultConfigAppSettingLinkedInCredentials()))
        {
            // pee you! this smells, but it works
            ServiceProvider = new BruteForceLinkedInClientServiceProvider(this);
        }
    }
}
