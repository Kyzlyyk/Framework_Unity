using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Kyzlyyk.Utils
{
    public struct JsonParser
    {
        public static string Parse(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public static bool TryParse(string filePath, out string json)
        {
            json = File.ReadAllText(filePath);

            return json != null;
        }

#nullable enable
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filePath"></param>
        /// <returns>Value of json.</returns>
        public static string? SelectKey(Predicate<string> key, string filePath)
        {
            JArray array = JArray.Parse(File.ReadAllText(filePath));

            JObject data = JObject.Parse(array[0].ToString());

            foreach (var item in data)
            {
#pragma warning disable
                if (key.Invoke(item.Key))
                    return item.Value.ToString();
#pragma warning restore
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="filePath"></param>
        /// <returns>Key of json.</returns>
        public static string? SelectValue(Predicate<string> value, string filePath)
        {
            JArray array = JArray.Parse(File.ReadAllText(filePath));

            JObject data = JObject.Parse(array[0].ToString());

            foreach (var item in data)
            {
#pragma warning disable
                if (value.Invoke(item.Value.ToString()))
                    return item.Key;
#pragma warning restore
            }

            return null;
        }
#nullable disable
    }
}