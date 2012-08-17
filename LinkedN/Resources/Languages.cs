using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class Languages
    {
        [DataMember(Name = "_total")]
        public int Total { get; set; }

        [DataMember(Name = "values")]
        public Language[] Values { get; set; }
    }
}
