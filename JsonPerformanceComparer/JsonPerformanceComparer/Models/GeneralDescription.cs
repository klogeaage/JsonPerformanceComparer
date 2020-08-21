using JsonPerformanceComparer.ViewModels;

namespace JourneyDoc.Models
{
    /// <summary>
    /// Misc. entities in the system will have the need for descriptions. This value object encapsulates that.
    /// </summary>
    public class GeneralDescription : NotifyPropertyChangedBase
    {
        private string _name;
        private string _description;
        private string _Comments;

        /// <summary>
        /// Name of item
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        /// <summary>
        /// A description
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        /// <summary>
        /// Comments given at start
        /// </summary>
        public string Comments
        {
            get { return _Comments; }
            set { SetProperty(ref _Comments, value); }
        }
    }
}