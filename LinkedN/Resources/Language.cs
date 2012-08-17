using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class Language
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "language")]
        public LanguageInfo LanguageInfo { get; set; }
    }
}
