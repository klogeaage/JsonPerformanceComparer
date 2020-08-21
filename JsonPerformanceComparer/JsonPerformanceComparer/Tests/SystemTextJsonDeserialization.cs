using JourneyDoc.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JsonPerformanceComparer.Tests
{
    public class SystemTextJsonDeserialization
    {
        public static async Task DeserializeFromStreamAsync(Stream stream)
        {
            var items = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<Journey>>(stream, JsonOptions.SystemTextJsonDefault);
        }
    }
}
