using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyDoc.Models.Interfaces
{
    /// <summary>
    /// Stuff we need in an entity to be able to save it in Azure table storage
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntityInTable<TKey>
    {
        /// <summary>
        /// The ETag to keep track of versions and updates
        /// </summary>
        string ETag { get; set; }

        /// <summary>
        /// Date when last update took place. May be caused by system operations
        /// </summary>
        public DateTimeOffset? LastUpdated { get; set; }

        /// <summary>
        /// Keep track of soft delete status
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Used for row key
        /// </summary>
        TKey Id { get; set; }
    }
}
