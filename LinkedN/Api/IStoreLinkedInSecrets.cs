namespace LinkedN
{
    /// <summary>
    /// This type is responsible for storing OAuth access token secrets provided by Linkedin.
    /// </summary>
    public interface IStoreLinkedInSecrets
    {
        IAuthenticateLinkedInApp AppCredentials { get; }

        void Create(string token, string secret);
        void Expire(string token);
        string Get(string token);
    }
}