using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyDoc.Common
{
    /// <summary>
    /// Some convenience functions to test for paths being local or remote.
    /// </summary>
    public static class PathStringExtensions
    {
        /// <summary>
        /// Returns true if this is a network URL.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsRemote(this string s)
        {
            return s != null && s.StartsWith("http");
        }

        /// <summary>
        /// Returns true if this is a local file
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsLocal(this string s)
        {
            return !IsRemote(s);
        }
    }
}
