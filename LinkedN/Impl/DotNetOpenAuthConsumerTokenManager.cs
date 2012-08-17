using System;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for implementing an interface required by the DotNetOpenAuth library.
    /// </summary>
    public class DotNetOpenAuthConsumerTokenManager : IConsumerTokenManager
    {
        private readonly IStoreLinkedInSecrets _secrets;

        public DotNetOpenAuthConsumerTokenManager(IStoreLinkedInSecrets secrets)
        {
            _secrets = secrets;
        }

        public string GetTokenSecret(string token)
        {
            return _secrets.Get(token);
        }

        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            _secrets.Create(response.Token, response.TokenSecret);
        }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            _secrets.Expire(requestToken);
            _secrets.Create(accessToken, accessTokenSecret);
        }

        public TokenType GetTokenType(string token)
        {
            throw new NotSupportedException();
        }

        public string ConsumerKey
        {
            get { return _secrets.AppCredentials.ApiKey; }
        }

        public string ConsumerSecret
        {
            get { return _secrets.AppCredentials.SecretKey; }
        }
    }
}
