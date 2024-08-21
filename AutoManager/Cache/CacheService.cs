using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AutoManager.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cacheService;

        public CacheService(IDistributedCache cacheService)
        {
            _cacheService = cacheService;
        }

        public T GetData<T>(string key)
        {
           var value = _cacheService.GetString(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public object RemoveData(string key)
        {
            _cacheService.Remove(key);
            return true;
        }

        public void SetData<T>(string key, T value, DateTime expirationTime)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = expirationTime,
            };
            _cacheService.SetString(key, JsonConvert.SerializeObject(value), options);
        }
    }
}
