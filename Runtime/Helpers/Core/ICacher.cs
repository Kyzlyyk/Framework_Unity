namespace Kyzlyk.Helpers.Core
{
    public interface ICacher<TValue, TIndexator>
    {
        TValue GetValue(TIndexator index);
        bool TryGetCachedValue(TIndexator index, out TValue cachedValue);
        
        void ToCache(TIndexator index);
    }
}