using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class HeaderValue
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}