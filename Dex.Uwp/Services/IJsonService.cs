using Newtonsoft.Json;

namespace Dex.Uwp.Services
{
    public interface IJsonService
    {
        T Deserialize<T>(string json);
    }

    public class JsonService : IJsonService
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}