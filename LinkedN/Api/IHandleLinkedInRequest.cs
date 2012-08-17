using System;
using System.Security.Principal;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for invoking a paramterized Linkedin REST API resource request.
    /// </summary>
    /// <typeparam name="TResource">
    /// The object model POCO representation of a resource request's expected schema.
    /// </typeparam>
    public interface IHandleLinkedInRequest<out TResource>
    {
        void SetRequestOption(Enum optionId, string value);
        string GetRequestOption(Enum optionId);
        string BuildRequestUrl();
        TResource SendRequestFrom(IPrincipal principal);
        TResource SendRequestFrom(string token);
    }
}