namespace AutoManager.Cache
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        void SetData<T>(string key, T value, DateTime expirationTime);
        object RemoveData(string key);
    }
}
