using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class Headers
    {
        [DataMember(Name = "_total")]
        internal int Total { get; set; }

        [DataMember(Name = "values")]
        internal HeaderValue[] Values { get; set; }
    }
}
