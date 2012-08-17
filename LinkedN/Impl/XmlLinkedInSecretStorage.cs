using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for implementing a required client interface using an XML file.
    /// </summary>
    public class XmlLinkedInSecretStorage : IStoreLinkedInSecrets
    {
        private bool _isFileInitialized;
        private static readonly object Lock = new object();
        private string _virtualPath = "~/App_Data/LinkedInTokenStorage.xml";
        private string _absolutePat;

        public XmlLinkedInSecretStorage(IAuthenticateLinkedInApp credentials)
        {
            AppCredentials = credentials;
        }

        public IAuthenticateLinkedInApp AppCredentials { get; private set; }

        public string VirtualPath
        {
            get { return _virtualPath; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                if (!value.StartsWith("~/"))
                    throw new ArgumentException("VirtualPath must start with the '~/' prefix.");
                _virtualPath = value;
            }
        }

        private string AbsolutePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_absolutePat))
                    _absolutePat = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, VirtualPath.Substring(2));
                return _absolutePat;
            }
        }

        private void InitializeXmlFile()
        {
            if (!_isFileInitialized && !File.Exists(AbsolutePath))
            {
                // make sure directory exists
                Debug.Assert(AbsolutePath != null);
                var directoryName = Path.GetDirectoryName(AbsolutePath);
                Debug.Assert(directoryName != null);
                if (!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);

                // create a blank file
                var fileStream = File.Create(AbsolutePath);
                fileStream.Dispose();

                // define XML and add root node
                using (var streamWriter = new StreamWriter(AbsolutePath))
                {
                    streamWriter.Write(@"<?xml version=""1.0"" encoding=""utf-8""?>
<LinkedInRestV1OAuth1aRequestTokens>
</LinkedInRestV1OAuth1aRequestTokens>");
                }
            }

            _isFileInitialized = true;
        }

        private XDocument LoadDocument()
        {
            InitializeXmlFile();
            return XDocument.Load(AbsolutePath);
        }

        public void Create(string token, string secret)
        {
            lock (Lock)
            {
                var document = LoadDocument();

                // check whether credential already exists
                Debug.Assert(document.Root != null);
                var found = document.Root.Descendants("Token")
                    .SingleOrDefault(e => e.Value == token);

                // overwrite secret if it already exists
                if (found != null)
                {
                    Debug.Assert(found.Parent != null);
                    found.Parent.Descendants("Secret").Single().Value = secret;
                }
                else
                {
                    var tokenNode = new XElement("Token", token);
                    var secretNode = new XElement("Secret", secret);
                    var bothNode = new XElement("Credential", tokenNode, secretNode);
                    document.Root.Add(bothNode);
                }

                document.Save(AbsolutePath);
            }
        }

        public void Expire(string token)
        {
            lock (Lock)
            {
                var document = LoadDocument();

                Debug.Assert(document.Root != null);
                var found = document.Root.Descendants("Token").SingleOrDefault(e => e.Value == token);
                if (found == null) throw new InvalidOperationException(string.Format("Token '{0}' is not recognized.", token));
                if (found.Parent == null) throw new InvalidOperationException(string.Format("Token '{0}' is not part of a credential pair.", token));
                found.Parent.Remove();

                document.Save(AbsolutePath);
            }
        }

        public string Get(string token)
        {
            lock (Lock)
            {
                var document = LoadDocument();

                Debug.Assert(document.Root != null);
                var found = document.Root.Descendants("Token").SingleOrDefault(e => e.Value == token);
                if (found == null) throw new InvalidOperationException(string.Format("Token '{0}' is not recognized.", token));
                if (found.Parent == null) throw new InvalidOperationException(string.Format("Token '{0}' is not part of a credential pair.", token));
                var secret = found.Parent.Descendants("Secret").SingleOrDefault();
                if (secret == null) throw new InvalidOperationException(string.Format("Token '{0}' does not have a matching secret.", token));
                return secret.Value;
            }
        }
    }
}
