using JourneyDoc.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace JsonPerformanceComparer.Tests
{

    public class NewtonsoftDeserialization
    {
        private readonly static JsonSerializer MySerializer = JsonSerializer.Create(JsonOptions.NewtonsoftDefault);

        public static void DeserializeFromStream(Stream stream)
        {
            // Not sure if we have a memory leak here, but any attempt at disposing the reader or the json will cause the underlying stream to be closed
            // So well live with that.
            var reader = new StreamReader(stream);
            var json = new JsonTextReader(reader);
            var items = MySerializer.Deserialize<IEnumerable<Journey>>(json);
        }
    }
}
