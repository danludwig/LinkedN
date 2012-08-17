using System;
using System.Security.Principal;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for implementing a required interface using DotNetOpenAuth.
    /// You can derive from this type or configure your IoC container to constructor-inject dependencies.
    /// </summary>
    public class LinkedInClient : IConsumeLinkedInApi
    {
        private IStoreLinkedInSecrets _secrets;
        private IConsumerTokenManager _consumerTokenManager;

        public LinkedInClient(IServiceProvider serviceProvider, IStoreLinkedInTokens tokens, IStoreLinkedInSecrets secrets)
        {
            ServiceProvider = serviceProvider;
            Tokens = tokens;
            Secrets = secrets;
        }

        internal readonly IStoreLinkedInTokens Tokens;
        internal IStoreLinkedInSecrets Secrets
        {
            get { return _secrets; }
            private set
            {
                _secrets = value;
                _consumerTokenManager = new DotNetOpenAuthConsumerTokenManager(_secrets);
            }
        }
        internal IServiceProvider ServiceProvider { get; set; }

        public virtual IHandleLinkedInRequest<TResource> ForResource<TResource>()
        {
            // create closed interface
            var implementedInterface = typeof(IHandleLinkedInRequest<>)
                .MakeGenericType(typeof(TResource));

            var service = ServiceProvider.GetService(implementedInterface);
            return (IHandleLinkedInRequest<TResource>)service;
        }

        public void RequestUserAuthorization(LinkedInUserAuthorizationRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

            var serviceDescription = ServiceDescriptionFactory.CreateUasDescription();
            var webConsumer = new WebConsumer(serviceDescription, _consumerTokenManager);
            var userAuthorizationRequest = webConsumer.PrepareRequestUserAuthorization(
                request.Callback, request.RequestParamaters, request.RedirectParamaters);
            webConsumer.Channel.Send(userAuthorizationRequest);
        }

        public LinkedInUserAuthorizationResponse ReceiveUserAuthorization(IPrincipal principal)
        {
            var serviceDescription = ServiceDescriptionFactory.CreateUasDescription();
            var webConsumer = new WebConsumer(serviceDescription, _consumerTokenManager);
            var accessTokenResponse = webConsumer.ProcessUserAuthorization();

            var apiResponse = !string.IsNullOrWhiteSpace(accessTokenResponse.AccessToken)
                ? new LinkedInUserAuthorizationResponse(accessTokenResponse) : null;

            if (Tokens != null && apiResponse != null)
                Tokens.Create(principal, apiResponse.Token);

            return apiResponse;
        }
    }
}
