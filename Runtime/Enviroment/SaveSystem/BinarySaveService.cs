using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kyzlyk.Enviroment.SaveSystem
{
    public sealed class BinarySaveService : ISaveService
    {
        public void DeleteData(ISaveable saveable)
        {
            SaveUtility.DeleteData(saveable.SaveKey);
        }

        public void SaveData(ISaveable saveable, object data)
        {
            BinaryFormatter formatter = new();

            using FileStream stream = new(SaveUtility.GeneratePath(saveable.SaveKey), FileMode.Create);

            formatter.Serialize(stream, data);
        }

        public bool TryLoadData<T>(ISaveable saveable, out T resultData, bool handleNullResult) where T : class
        {
            string path = SaveUtility.GeneratePath(saveable.SaveKey);

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new();
                using (FileStream stream = new(path, FileMode.Open))
                {
                    resultData = formatter.Deserialize(stream) as T;
                }

                return !handleNullResult || resultData != null;
            }

            //Debug.LogAssertion("The file not found in " + path);
            resultData = null;

            return false;
        }
    }
}
