namespace Kyzlyk.Enviroment.SaveSystem
{
    public interface ISaveable
    {
        string SavedObjectKey { get; }

        void SaveData();
        void LoadSavedData();
        void DeleteSavedData();
    }
}