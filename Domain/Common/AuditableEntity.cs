using System;

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
        public DateTime? CreatedUtc { get; set; }

        /// <summary>
        /// User for when the entity was created
        /// </summary>
       // public long CreatedUser { get; set; }
        public Guid? CreatedUser { get; set; }

        /// <summary>
        /// UTC time for when the entity was last modified
        /// </summary>
        public DateTime? LastModifiedUtc { get; set; }

        /// <summary>
        /// User for when the entity was last modified
        /// </summary>
       // public long LastModifiedUser { get; set; }
        public Guid? LastModifiedUser { get; set; }

    }
}
