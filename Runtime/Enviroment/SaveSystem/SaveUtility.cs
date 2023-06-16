using System;
using System.IO;
using UnityEngine;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kyzlyk.Enviroment.SaveSystem
{
    internal static class SaveUtility
    {
        private static string GeneratePath(string fileName) => Application.persistentDataPath + "/" + fileName + ".ky";

        public static void SaveDateTime(string key, DateTime value)
        {
            string dateTimeString = value.ToString("u", CultureInfo.InvariantCulture);
            PlayerPrefs.SetString(key, dateTimeString);
        }

        public static DateTime LoadDateTime(string key, DateTime defaultValue)
        {
            if (PlayerPrefs.HasKey(key))
            {
                string stored = PlayerPrefs.GetString(key);
                return DateTime.ParseExact(stored, "u", CultureInfo.InvariantCulture);
            }
            else
            {
                return defaultValue;
            }
        }

        public static void DeleteDateTime(string key) => PlayerPrefs.DeleteKey(key);

        public static void SaveData(string key, object data)
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(GeneratePath(key), FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static bool TryLoadData<T>(string key, out T resultData) where T : class
        {
            string path = GeneratePath(key);
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new();
                FileStream stream = new(path, FileMode.Open);

                resultData = formatter.Deserialize(stream) as T;
                stream.Close();

                return true;
            }

            //Debug.LogError("The file not found in " + path);
            resultData = null;

            return false;
        }

        public static void DeleteData(string key)
        {
            string path = GeneratePath(key);
            File.Delete(path);
        }
    }
}