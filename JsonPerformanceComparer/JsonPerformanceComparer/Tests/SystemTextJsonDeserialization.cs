using JourneyDoc.Models;
using JsonPerformanceComparer.Models.JsonContext;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JsonPerformanceComparer.Tests
{
    public class SystemTextJsonDeserialization
    {
        internal static JourneyDocJsonContext journeyDocJsonContext = new(JsonOptions.SystemTextJsonDefault);

        public static async Task DeserializeFromStreamAsync(Stream stream)
        {
            var items = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<Journey>>(stream, journeyDocJsonContext.IEnumerableJourney);
        }
    }
}
