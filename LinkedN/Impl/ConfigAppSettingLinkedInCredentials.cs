using System;
using System.Configuration;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for implementing a required client interface using the appSettings section of a configuration file.
    /// </summary>
    public class ConfigAppSettingLinkedInCredentials : IAuthenticateLinkedInApp
    {
        private readonly string _apiKeyName;
        private readonly string _secretKeyName;

        public ConfigAppSettingLinkedInCredentials(string apiKeyName, string secretKeyName)
        {
            if (string.IsNullOrWhiteSpace(apiKeyName)) throw new ArgumentException("Cannot be null or white space.", "apiKeyName");
            if (string.IsNullOrWhiteSpace(secretKeyName)) throw new ArgumentException("Cannot be null or white space.", "secretKeyName");
            _apiKeyName = apiKeyName;
            _secretKeyName = secretKeyName;
        }

        public string ApiKey
        {
            get { return ConfigurationManager.AppSettings[_apiKeyName]; }
        }

        public string SecretKey
        {
            get { return ConfigurationManager.AppSettings[_secretKeyName]; }
        }
    }
}
