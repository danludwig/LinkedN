using System.Runtime.Serialization;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for providing an object model representation of the Linkedin Person resource schema.
    /// </summary>
    [DataContract]
    public class Person : ResourceModel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "maidenName")]
        public string MaidenName { get; set; }

        [DataMember(Name = "headline")]
        public string Headline { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "connections")]
        public Connections Connections { get; set; }

        [DataMember(Name = "educations")]
        public Educations Educations { get; set; }

        [DataMember(Name = "industry")]
        public string Industry { get; set; }

        [DataMember(Name = "languages")]
        public Languages Languages { get; set; }

        [DataMember(Name = "apiPublicProfileRequest")]
        internal ProfileRequest ApiPublicProfileRequest { get; set; }

        [DataMember(Name = "apiStandardProfileRequest")]
        internal ProfileRequest ApiStandardProfileRequest { get; set; }

        [DataMember(Name = "siteStandardProfileRequest")]
        internal ProfileRequest SiteStandardProfileRequest { get; set; }
    }
}
