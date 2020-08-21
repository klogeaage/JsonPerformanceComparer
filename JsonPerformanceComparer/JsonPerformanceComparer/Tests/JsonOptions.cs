
namespace JsonPerformanceComparer.Tests
{
    public static class JsonOptions
    {
        public static Newtonsoft.Json.JsonSerializerSettings NewtonsoftDefault = new Newtonsoft.Json.JsonSerializerSettings
        {
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            Formatting = Newtonsoft.Json.Formatting.None,

        };

        public static System.Text.Json.JsonSerializerOptions SystemTextJsonDefault = new System.Text.Json.JsonSerializerOptions
        {
            IgnoreReadOnlyProperties = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
        };
    }
}
