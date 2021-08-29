using JourneyDoc.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JsonPerformanceComparer.Models.JsonContext
{
    [JsonSerializable(typeof(IEnumerable<Journey>))]
    internal partial class JourneyDocJsonContext : JsonSerializerContext
    {
    }
}
