using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class Educations
    {
        [DataMember(Name = "_total")]
        public int Total { get; set; }

        [DataMember(Name = "values")]
        public Education[] Values { get; set; }
    }
}
