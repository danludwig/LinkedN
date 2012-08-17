using System.Runtime.Serialization;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for encapsulating properties common to all POCO resource models.
    /// </summary>
    [DataContract]
    public abstract class ResourceModel
    {
        public string Uri { get; internal set; }
        public string Resource { get; internal set; }
    }
}
