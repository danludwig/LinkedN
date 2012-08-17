using System.Runtime.Serialization;

namespace LinkedN
{
    [DataContract]
    public class Education
    {
        [DataMember(Name = "activities")]
        public string Activities { get; set; }

        [DataMember(Name = "degree")]
        public string Degree { get; set; }

        [DataMember(Name = "endDate")]
        public Date EndDate { get; set; }

        [DataMember(Name = "fieldOfStudy")]
        public string FieldOfStudy { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "notes")]
        public string Notes { get; set; }

        [DataMember(Name = "schoolName")]
        public string SchoolName { get; set; }

        [DataMember(Name = "startDate")]
        public Date StartDate { get; set; }
    }
}
