using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class LanguageInfo
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
