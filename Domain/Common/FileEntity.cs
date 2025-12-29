using System;

namespace Domain.Common
{
    /// <summary>
    /// Entity base class
    /// </summary>
    public abstract class FileEntity : AuditableEntity
    {
        /// <summary>
        /// Unique identifier to identify specific instance
        /// </summary>
        ///  public Guid FileGuid { get; set; } = Guid.NewGuid();
        public Guid FileGuid { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public byte[] DataFiles { get; set; } = null!;
    }
}
