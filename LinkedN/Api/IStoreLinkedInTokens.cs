using System.Security.Principal;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for storing OAuth access tokens provided by Linkedin, identifyable by security principal.
    /// </summary>
    public interface IStoreLinkedInTokens
    {
        void Create(IPrincipal principal, string token);
        void Expire(IPrincipal principal);
        string Get(IPrincipal principal);
    }
}