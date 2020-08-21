using JourneyDoc.Models.Interfaces;
using System;

namespace JourneyDoc.Models
{
    /// <summary>
    /// Tags can be linked to various entities in the system.
    /// </summary>
    public class Tag : ItemBase<int>, IPresentableItem
    {
        private string _presentationText;

        /// <summary>
        /// Name and title
        /// </summary>
        public GeneralDescription GeneralDescription { get; set; } = new GeneralDescription();

        /// <summary>
        /// Used for sorting. Phases especially should be sorted according to this.
        /// </summary>
       public int SortIndex { get; set; }

        /// <summary>
        /// Type of tag.
        /// </summary>
        public TagTypes Type { get; set; }

        /// <summary>
        /// Text used in presentation mode
        /// </summary>
        public string PresentationText
        {
            get { return _presentationText; }
            set { SetProperty(ref _presentationText, value); }
        }

        /// <summary>
        /// Not currently used
        /// </summary>
        public string ImagePath { get; }

        /// <summary>
        /// Path to sound. Currently not used.
        /// </summary>
        public string SoundPath { get; }
        /// <summary>
        /// Not currently used
        /// </summary>
        public string VideoPath { get; }

        /// <summary>
        /// Should phase be included in presentation
        /// </summary>
        public bool IncludeInPresentation { get; set; }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        public DateTimeOffset? Start => null;

        /// <summary>
        /// The group name in presentation mode.
        /// </summary>
        public string GroupName => "Phase";

        /// <summary>
        /// True if a description or presentation text exist.
        /// </summary>
        public bool HasPresentationText
        {
            get
            {
                return !string.IsNullOrEmpty(GeneralDescription.Description) || !string.IsNullOrEmpty(PresentationText);
            }
        }

        /// <summary>
        /// Convenient for display in lists
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GeneralDescription.Name;
        }
    }
}