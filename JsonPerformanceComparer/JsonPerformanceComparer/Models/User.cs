using JourneyDoc.Models.Interfaces;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JourneyDoc.Models
{
    /// <summary>
    /// Details about a user. All users belong to at least one account.
    /// </summary>
    public class User : ItemBase<string>, IEntityInTable<string>
    {
        private string _name;
        private string _mobilePhoneNumber;
        private UserTypes _userType;
        private bool _isDisabled;
        private string _notes;
        private bool _isAdmin;
        private UserSettings _userSettings;

        /// <summary>
        /// Full name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        /// <summary>
        /// Email address. Also used to identify user during invitation phase.
        /// </summary>
        [EmailAddress]
        [Required]
        public string Email
        {
            get { return Id; }
        }

        /// <summary>
        /// Mobile phone
        /// </summary>
        public string MobilePhoneNumber
        {
            get { return _mobilePhoneNumber; }
            set { SetProperty(ref _mobilePhoneNumber, value); }
        }

        /// <summary>
        /// Misc. notes about the user
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }
        /// <summary>
        /// Type of user
        /// </summary>
        public UserTypes UserType
        {
            get { return _userType; }
            set { SetProperty(ref _userType, value); }
        }

        /// <summary>
        /// Enum flags with various user settings
        /// </summary>
        public UserSettings UserSettings { get => _userSettings; set => SetProperty(ref _userSettings, value); }

        /// <summary>
        /// Is the user an admin user with rights to templates and demo workspaces?
        /// </summary>
        public bool IsAdmin { get => _isAdmin; set =>  SetProperty(ref _isAdmin, value); }
        
        /// <summary>
        /// Is the user active or disabled in the system.
        /// </summary>
        public bool IsDisabled
        {
            get { return _isDisabled; }
            set { SetProperty(ref _isDisabled, value); }
        }

        /// <summary>
        /// All accounts the user is found in.
        /// </summary>
        public IList<string> AccountIds { get; set; } = new List<string>();

        /// <summary>
        /// The internal Object Id in Azure AD
        /// </summary>
        public string Oid { get; set; }

        /// <summary>
        /// Has the user been authenticated?
        /// </summary>
        public bool HasAuthenticated  => Oid != null;

        /// <summary>
        /// List of platforms, i.e. Android, iOS, etc. the user has been using.
        /// </summary>
        public IList<string> Platforms { get; set; } = new List<string>();

        /// <summary>
        /// List of devices the user has been using.
        /// </summary>
        public IList<string> Devices { get; set; } = new List<string>();

        /// <summary>
        /// When has user last signed in?
        /// </summary>
        public DateTimeOffset? LastSignIn { get; set; }

        /// <summary>
        /// The current workspace is the one loaded after signin.
        /// </summary>
        public string CurrentWorkspaceId { get; set; }

        /// <summary>
        /// The current journey, i.e. last opened.
        /// </summary>
        public string CurrentJourneyId { get; set; }

        /// <summary>
        /// For simple display in a list
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}