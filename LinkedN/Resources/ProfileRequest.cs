using System.Runtime.Serialization;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for providing an object model representation of a Linkedin Person's nested resource schema.
    /// </summary>
    [DataContract]
    internal class ProfileRequest
    {
        [DataMember(Name = "url")]
        internal string Url { get; set; }

        [DataMember(Name = "headers")]
        internal Headers HeadersInternal { get; set; }
    }
}
