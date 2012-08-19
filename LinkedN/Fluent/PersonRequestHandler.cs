using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Text;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for implementing a required generic interface using DotNetOpenAuth.
    /// </summary>
    public class PersonRequestHandler : IHandleLinkedInRequest<Person>
    {
        private readonly IStoreLinkedInTokens _tokens;
        private readonly IConsumerTokenManager _consumerTokenManager;

        public PersonRequestHandler(IStoreLinkedInTokens tokens, IStoreLinkedInSecrets secrets)
        {
            _tokens = tokens;
            _consumerTokenManager = new DotNetOpenAuthConsumerTokenManager(secrets);
            _requestOptions = new Dictionary<Enum, string>();
        }

        private readonly IDictionary<Enum, string> _requestOptions;

        public void SetRequestOption(Enum optionId, string value)
        {
            _requestOptions[optionId] = value;
        }

        public string GetRequestOption(Enum optionId)
        {
            return _requestOptions.ContainsKey(optionId)
                ? _requestOptions[optionId] : null;
        }

        public const string BaseUrl = "http://api.linkedin.com/v1/people/";

        public string BuildRequestUrl()
        {
            var builder = new StringBuilder(BaseUrl);

            // get identifier
            var identifier = GetRequestOption(PersonRequestOption.Identification);
            if (string.IsNullOrWhiteSpace(identifier))
                throw new InvalidOperationException("You must specify which person profile being requested: either 'Myself', 'MemberId', or 'MemberUrl'.");
            builder.Append(identifier);

            // append public, if necessary
            var profileVersion = GetRequestOption(PersonRequestOption.ProfileVersion);
            if (!string.IsNullOrWhiteSpace(profileVersion))
                builder.Append(profileVersion);

            // select fields
            builder.Append(GetRequestOption(PersonRequestOption.Fields));

            builder.Append("?format=json");
            return builder.ToString();
        }

        public Person SendRequestFrom(IPrincipal principal)
        {
            return SendRequestFrom(_tokens.Get(principal));
        }

        public Person SendRequestFrom(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("An OAuth token is required to send this request.");
            var serviceDescription = ServiceDescriptionFactory.CreateUasDescription();
            var webConsumer = new WebConsumer(serviceDescription, _consumerTokenManager);
            var endpointUrl = BuildRequestUrl();
            var messageReceivingEndpoint = new MessageReceivingEndpoint(endpointUrl, HttpDeliveryMethods.GetRequest);
            var webRequest = webConsumer.PrepareAuthorizedRequest(messageReceivingEndpoint, token);
            var acceptLanguage = GetRequestOption(PersonRequestOption.Languages);
            if (!string.IsNullOrWhiteSpace(acceptLanguage))
                webRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, acceptLanguage);
            var webResponse = webRequest.GetResponse();
            var resource = webResponse.GetResponseStream().ConvertTo<Person>(endpointUrl);
            return resource;
        }
    }
}