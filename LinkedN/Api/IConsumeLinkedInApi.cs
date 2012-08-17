using System.Security.Principal;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for encapsulating communication between the client and Linkedin's OAuth endpoints.
    /// </summary>
    public interface IConsumeLinkedInApi
    {
        void RequestUserAuthorization(LinkedInUserAuthorizationRequest request);
        LinkedInUserAuthorizationResponse ReceiveUserAuthorization(IPrincipal principal);
        IHandleLinkedInRequest<TResource> ForResource<TResource>();
    }
}