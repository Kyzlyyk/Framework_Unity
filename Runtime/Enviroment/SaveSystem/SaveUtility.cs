using System;
using System.IO;
using UnityEngine;
using System.Globalization;
using System.Text;

namespace Kyzlyk.Enviroment.SaveSystem
{
    public static class SaveUtility
    {
        internal const string Extension = ".ky";
        
        internal static string GeneratePath(string fileName)
        {
            return GeneratePath(fileName, Application.persistentDataPath);
        }
        
        internal static string GeneratePath(string fileName, string root)
        {
            StringBuilder builder = new(fileName.Length + Extension.Length + root.Length + 1);
            builder
                .Append(root)
                .Append('/')
                .Append(fileName)
                .Append(Extension);

            return builder.ToString();
        }
        
        public static void SaveDateTime(ISaveable saveable, DateTime value)
        {
            string dateTimeString = value.ToString("u", CultureInfo.InvariantCulture);

            new TimeSaver().SaveData(saveable, dateTimeString);
        }

        public static DateTime LoadDateTime(ISaveable saveable, DateTime defaultValue)
        {
            TimeSaver timeSave = new();
            if (timeSave.TryLoadData(saveable, out string date))
            {
                return DateTime.ParseExact(date, "u", CultureInfo.InvariantCulture);
            }
            else
            {
                return defaultValue;
            }
        }

        public static void DeleteDateTime(ISaveable saveable) 
            => new TimeSaver().DeleteData(saveable);

        public static void DeleteData(string key)
        {
            File.Delete(GeneratePath(key));
        }

        private readonly struct TimeSaver : ISaveService
        {
            private ISaveService InternalSaveService => new BinarySaveService();

            public void SaveData(ISaveable saveable, object data)
            {
                InternalSaveService.SaveData(saveable, data);
            }

            public bool TryLoadData<T>(ISaveable saveable, out T resultData, bool handleNullResult = true) where T : class
            {
                 return InternalSaveService.TryLoadData(saveable, out resultData, handleNullResult);
            }

            public void DeleteData(ISaveable saveable)
            {
                InternalSaveService.DeleteData(saveable);
            }
        }
    }
}