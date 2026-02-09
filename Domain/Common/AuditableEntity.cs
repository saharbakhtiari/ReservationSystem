using System;
using System.Text.Json.Serialization;

namespace Domain.Common
{
    /// <summary>
    /// A base class definition for Entities / Domain Models.
    /// Perhaps in the future we will changed this to include a list of complete audit records! But not just yet.
    /// </summary>
    public abstract class AuditableEntity : Entity
    {

        /// <summary>
        /// UTC time for when the entity was created
        /// </summary>
        [JsonIgnore]
        public DateTime? CreatedUtc { get; set; }

        /// <summary>
        /// User for when the entity was created
        /// </summary>
        [JsonIgnore]
        public Guid? CreatedUser { get; set; }

        /// <summary>
        /// UTC time for when the entity was last modified
        /// </summary>
        [JsonIgnore]
        public DateTime? LastModifiedUtc { get; set; }

        /// <summary>
        /// User for when the entity was last modified
        /// </summary>
        [JsonIgnore]
        public Guid? LastModifiedUser { get; set; }

    }
}
