using System.Runtime.Serialization;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for strongly-typing all person fields available from the Linkedin REST API.
    /// </summary>
    public enum PersonField
    {
        [DataMember(Name = "id")]
        Id,

        [DataMember(Name = "first-name")]
        FirstName,

        [DataMember(Name = "last-name")]
        LastName,

        [DataMember(Name = "maiden-name")]
        MaidenName,

        [DataMember(Name = "formatted-name")]
        FormattedName,

        [DataMember(Name = "phonetic-first-name")]
        PhoneticFirstName,

        [DataMember(Name = "phonetic-last-name")]
        PhoneticLastName,

        [DataMember(Name = "formatted-phonetic-name")]
        FormattedPhoneticName,

        [DataMember(Name = "headline")]
        Headline,

        [DataMember(Name = "location")]
        Location,

        [DataMember(Name = "distance")]
        Distance,

        [DataMember(Name = "last-modified-timestamp")]
        LastModifiedTimestamp,

        [DataMember(Name = "current-share")]
        CurrentShare,

        [DataMember(Name = "network")]
        Network,

        [DataMember(Name = "connections")]
        Connections,

        [DataMember(Name = "num-connections")]
        NumConnections,

        [DataMember(Name = "num-connections-capped")]
        NumConnectionsCapped,

        [DataMember(Name = "summary")]
        Summary,

        [DataMember(Name = "specialties")]
        Specialties,

        [DataMember(Name = "proposal-comments")]
        ProposalComments,

        [DataMember(Name = "associations")]
        Associations,

        [DataMember(Name = "honors")]
        Honors,

        [DataMember(Name = "interests")]
        Interests,

        [DataMember(Name = "positions")]
        Positions,

        [DataMember(Name = "publications")]
        Publications,

        [DataMember(Name = "patents")]
        Patents,

        [DataMember(Name = "languages")]
        Languages,

        [DataMember(Name = "skills")]
        Skills,

        [DataMember(Name = "certifications")]
        Certifications,

        [DataMember(Name = "educations")]
        Educations,

        [DataMember(Name = "courses")]
        Courses,

        [DataMember(Name = "volunteer")]
        Volunteer,

        [DataMember(Name = "three-current-positions")]
        ThreeCurrentPositions,

        [DataMember(Name = "three-past-positions")]
        ThreePastPositions,

        [DataMember(Name = "num-recommenders")]
        NumRecommenders,

        [DataMember(Name = "recommendations-received")]
        RecommendationsReceived,

        [DataMember(Name = "phone-numbers")]
        PhoneNumbers,

        [DataMember(Name = "im-accounts")]
        ImAccounts,

        [DataMember(Name = "twitter-accounts")]
        TwitterAccounts,

        [DataMember(Name = "primary-twitter-account")]
        PrimaryTwitterAccount,

        [DataMember(Name = "bound-account-types")]
        BoundAccountTypes,

        [DataMember(Name = "mfeed-rss-url")]
        MfeedRsUrl,

        [DataMember(Name = "following")]
        Following,

        [DataMember(Name = "job-bookmarks")]
        JobBookmarks,

        [DataMember(Name = "group-memberships")]
        GroupMemberships,

        [DataMember(Name = "suggestions")]
        Suggestions,

        [DataMember(Name = "date-of-birth")]
        DateOfBirth,

        [DataMember(Name = "main-address")]
        MainAddress,

        [DataMember(Name = "member-url-resources")]
        MemberUrlResources,

        [DataMember(Name = "picture-url")]
        PictureUrl,

        [DataMember(Name = "site-standard-profile-request")]
        SiteStandardProfileRequest,

        [DataMember(Name = "api-standard-profile-request")]
        ApiStandardProfileRequest,

        [DataMember(Name = "api-public-profile-request")]
        ApiPublicProfileRequest,

        [DataMember(Name = "public-profile-url")]
        PublicProfileUrl,

        [DataMember(Name = "related-profile-views")]
        RelatedProfileViews,
    }
}