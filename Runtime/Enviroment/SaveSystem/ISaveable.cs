namespace Kyzlyk.Enviroment.SaveSystem
{
    internal interface ISaveable
    {
        string SavedObjectKey { get; }

        void SaveData();
        void LoadSavedData();
        void DeleteSavedData();
    }
}