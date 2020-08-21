using JsonPerformanceComparer.ViewModels;
using System;
using System.Threading.Tasks;

namespace JourneyDoc.Models
{
    /// <summary>
    /// Base class for all entity types 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ItemBase<T> : NotifyPropertyChangedBase
    {
        /// <summary>
        /// The Id of an item
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// The Etag from table storage to keep track of updates.
        /// </summary>
        public string ETag { get; set; }

        /// <summary>
        /// Date when last update took place. May be caused by system operations
        /// </summary>
        public DateTimeOffset? LastUpdated { get; set; }

        /// <summary>
        /// Keep track of soft delete status
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Implement this if you want things to happen on the client after object is received from server.
        /// </summary>
        public virtual Task PostClientSerializationAsync(object owner = null)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Implement this if you want things to happen before serializing and sending it to server
        /// </summary>
        public virtual void PreClientSerialization() { }
    }
}
