using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class Location
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "country")]
        public Country Country { get; set; }
    }
}
