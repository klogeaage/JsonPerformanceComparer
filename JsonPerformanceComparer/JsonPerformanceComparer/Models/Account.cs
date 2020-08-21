using JourneyDoc.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace JourneyDoc.Models
{
    /// <summary>
    /// For historic reasons, this is still called an account.
    /// In the user interface, it is known as a Workspace.
    /// It connects users and journeys for a specific purpose.
    /// </summary>
    public class Account : ItemBase<string>, IEntityInTable<string>
    {
        private string _ownerId;
        private bool _isDemo;

        /// <summary>
        /// The Id of the user that is the owner of the workspace.
        /// </summary>
        public string OwnerId
        {
            get { return _ownerId; }
            set
            {
                SetProperty(ref _ownerId, value);
                OnPropertyChanged(nameof(Owner));
            }
        }

        /// <summary>
        /// Owner of the workspace, i.e. responsible for it and its users.
        /// </summary>
        public User Owner
        {
            get
            {
                return Users.FirstOrDefault(x => x.Id == OwnerId);
            }
        }

        /// <summary>
        /// Does user with id userId have owner priviligies in the workspace?  
        /// </summary>
        /// <param name="userId">Id of user in question</param>
        /// <returns></returns>
        public bool HasOwnerPermissions(string userId)
        {
            return OwnerId == userId || Users.Any(x => x.Id == userId && x.UserType == UserTypes.CoOwner);
        }

        /// <summary>
        /// Name and description
        /// </summary>
        public GeneralDescription GeneralDescription { get; set; } = new GeneralDescription();

        /// <summary>
        /// List of all users (including owner)
        /// </summary>
        public IList<User> Users { get; set; } = new List<User>();

        /// <summary>
        /// Is this a demo/sample workspace
        /// </summary>
        public bool IsDemo { get => _isDemo; set => SetProperty(ref _isDemo, value); }

        /// <summary>
        /// To make it easier to use in views.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GeneralDescription.Name;
        }
    }
}