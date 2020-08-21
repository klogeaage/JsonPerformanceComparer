using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JourneyDoc.Models
{
    /// <summary>
    /// Tags defined by system
    /// </summary>
    public enum TagTypes
    {
        /// <summary>
        /// Tags designating phases
        /// </summary>
        Phase = 0,
        /// <summary>
        /// Tags for steps (not currently used)
        /// </summary>
        Step = 1,
        /// <summary>
        /// A user defined tag will have this type
        /// </summary>
        UserDefined = 2
    }

    /// <summary>
    /// The resolution of imported images.
    /// Should match the media plugins similar definition (PhotoSize).
    /// </summary>
    public enum ImageResolution
    {
        /// <summary>
        /// 25% of size, may be good enough in many cases?
        /// </summary>
        Small,
        /// <summary>
        /// 50% of size. This is the default
        /// </summary>
        Medium,
        /// <summary>
        /// 75% of size
        /// </summary>
        Large,
        /// <summary>
        /// Full size. 
        /// </summary>
        Full
    }

    /// <summary>
    /// Type of user
    /// </summary>
    public enum UserTypes
    {
        /// <summary>
        /// Used to denote a user is a guest in this account
        /// </summary>
        Guest,
        /// <summary>
        /// Regular user
        /// </summary>
        Member,
        /// <summary>
        /// Co-owner (administrator) of account, i.e. has same privileges as owner.
        /// </summary>
        CoOwner,
    }

    /// <summary>
    /// Controls various user settings
    /// </summary>
    [Flags]
    public enum UserSettings
    {
        /// <summary>
        /// Should list of workspaces include demo workspaces?
        /// </summary>
        ShowDemoWorkspaces = 1,
        /// <summary>
        /// Should we also store photos taken with JourneyDoc in the user's own photos?
        /// </summary>
        StorePhotosInUserSpace = 2
    }

}
