namespace LinkedN
{
    /// <summary>
    /// This type is responsible for providing an out-of-the-box naming convention for a configuration file's appSettings section.
    /// </summary>
    public class DefaultConfigAppSettingLinkedInCredentials : ConfigAppSettingLinkedInCredentials
    {
        public DefaultConfigAppSettingLinkedInCredentials()
            : base("LinkedInRestV1OAuth1aApiKey", "LinkedInRestV1OAuth1aSecretKey") { }
    }
}
