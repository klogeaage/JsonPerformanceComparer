using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyDoc.Models
{
    /// <summary>
    /// Data describing physical location
    /// </summary>
    public class LocationData
    {
        /// <summary>
        /// GPS latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// GPS longitude
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Admin area, like region or state
        /// </summary>
        public string AdminArea { get; set; }

        /// <summary>
        /// Full name of country
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// ISO country code, e.g. DK
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Best guess at an street name and number
        /// </summary>
        public string FeatureName { get; set; }

        /// <summary>
        /// Best guess at a city
        /// </summary>
        public string Locality { get; set; }

        /// <summary>
        /// Postal (Zip) code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Sub admin area, like Bari or Nordsjælland
        /// </summary>
        public string SubAdminArea { get; set; }

        /// <summary>
        /// Sub locality, often not set.
        /// </summary>
        public string SubLocality { get; set; }

        /// <summary>
        /// Get a rather crudely formatted address. 
        /// Should be enhanced to take country code into account.
        /// </summary>
        /// <returns></returns>
        public string Address
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine(FeatureName);
                if (SubLocality != null)
                    sb.AppendLine(SubLocality);
                sb.AppendLine($"{PostalCode} {Locality}");
                sb.Append(CountryName);
                return sb.ToString();
            }
        }
    }
}
