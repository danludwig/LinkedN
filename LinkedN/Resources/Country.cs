using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class Country
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }
    }
}
