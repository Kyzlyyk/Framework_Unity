namespace Kyzlyk.Enviroment.SaveSystem
{
    public interface ISaveService
    {
        void SaveData(ISaveable saveable, object data);
        bool TryLoadData<T>(ISaveable saveable, out T resultData, bool handleNullResult = true) where T : class;
        void DeleteData(ISaveable saveable);
    }
}
