using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SqaConferenceService.Test.Extensions
{
    internal static class JsonSerializerExtensions
    {
        internal static string AsJson(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            });
            return json;
        }
    }
}
