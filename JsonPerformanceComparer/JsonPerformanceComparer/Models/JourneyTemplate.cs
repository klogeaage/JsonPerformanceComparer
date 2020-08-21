using JourneyDoc.Models.Interfaces;
using System.Collections.Generic;

namespace JourneyDoc.Models
{
    /// <summary>
    /// Information used to classify and create a journey from.
    /// </summary>
    public class JourneyTemplate : ItemBase<int>, IEntityInTable<int>
    {
        /// <summary>
        /// General description. Used to inform the user about what the journeys based on this are about.
        /// </summary>
        public GeneralDescription GeneralDescription { get; set; }

        /// <summary>
        /// User responsible for the this
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// Id of user responsible for this?
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// List of tags. Built from TagIds on demand.
        /// Not transmitted in JSON and should eventually be ignored
        /// (not possible right now, but when all targets are .NET Core 3.0, it might).
        /// </summary>
        public IList<Tag> Tags { get; set; } = new List<Tag>();

        /// <summary>
        /// How high a resolution should images imported in to journeys based on this template have?
        /// </summary>
        public ImageResolution ImageResolution { get; set; }

        /// <summary>
        /// Should we track GPS location for points added to journeys based on this template?
        /// </summary>
        public bool IsLocationTrackingEnabled { get; set; }
    }
}