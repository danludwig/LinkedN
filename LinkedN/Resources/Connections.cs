using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class Connections
    {
        [DataMember(Name = "_total")]
        public int Total { get; set; }
    }
}
