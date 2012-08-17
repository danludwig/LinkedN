namespace LinkedN
{
    /// <summary>
    /// This type is responsible for accessing your application's Linkedin OAuth consumer credentials.
    /// </summary>
    public interface IAuthenticateLinkedInApp
    {
        string ApiKey { get; }
        string SecretKey { get; }
    }
}
