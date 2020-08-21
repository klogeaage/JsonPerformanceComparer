using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyDoc.Models.Dto
{
    /// <summary>
    /// Various settings for the user that we want to update separately
    /// </summary>
    public class UserSettingsDto
    {
        /// <summary>
        /// Enum flags with various user settings.
        /// Passed as int here to get Swagger right.
        /// </summary>
        public int UserSettings { get; set; }

        /// <summary>
        /// The current workspace is the one loaded after signin.
        /// </summary>
        public string CurrentWorkspaceId  { get; set; }

        /// <summary>
        /// The current journey, i.e. last opened.
        /// </summary>
        public string CurrentJourneyId { get; set; }
    }
}
