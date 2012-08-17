using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class Date
    {
        [DataMember(Name = "year")]
        public int Year { get; set; }
    }
}
