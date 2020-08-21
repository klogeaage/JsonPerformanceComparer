using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyDoc.Models.Dto
{
    /// <summary>
    /// Combination of user and account information
    /// </summary>
    public class UserAccountDto 
    {
        /// <summary>
        /// The user
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The user's account
        /// </summary>
        public IList<Account> Accounts { get; set; } = new List<Account>();
    }
}
