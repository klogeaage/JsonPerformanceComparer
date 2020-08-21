using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyDoc.Models.Interfaces
{
    /// <summary>
    /// If an item should be supported in presentation mode, it must implement this.
    /// </summary>
    public interface IPresentableItem
    {
        /// <summary>
        /// General description
        /// </summary>
        GeneralDescription GeneralDescription { get; }

        /// <summary>
        /// Text used in presentation mode if present.
        /// </summary>
        string PresentationText { get; set; }


        /// <summary>
        /// Start date for journey, phase or point
        /// </summary>
        public DateTimeOffset? Start { get; }

        /// <summary>
        /// Image path
        /// </summary>
        string ImagePath { get; }

        /// <summary>
        /// Sound path
        /// </summary>
        string SoundPath { get; }

        /// <summary>
        /// Video path
        /// </summary>
        string VideoPath { get; }

        /// <summary>
        /// The group name, if any
        /// </summary>
        public string GroupName { get; }

        /// <summary>
        /// Should this be shown as a separate slide in presentation mode?
        /// </summary>
        public bool IncludeInPresentation { get; }

        /// <summary>
        /// True if a description or presentation text exist.
        /// </summary>
        public bool HasPresentationText { get; }
    }
}
