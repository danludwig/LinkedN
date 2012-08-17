using System.Collections.Generic;
using DotNetOpenAuth.OAuth.Messages;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for encapsulating consumable data returned by a Linkedin OAuth user authorization request.
    /// </summary>
    public class LinkedInUserAuthorizationResponse : Dictionary<string, string>
    {
        private LinkedInUserAuthorizationResponse(string token, IDictionary<string, string> extraData = null)
        {
            Token = token;
            if (extraData == null) return;
            foreach (var key in extraData.Keys)
                this[key] = extraData[key];
        }

        internal LinkedInUserAuthorizationResponse(AuthorizedTokenResponse dnoaResponse)
            : this(dnoaResponse.AccessToken, dnoaResponse.ExtraData)
        {
        }

        public string Token { get; private set; }
    }
}
