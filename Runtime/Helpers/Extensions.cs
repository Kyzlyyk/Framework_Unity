using System;
using UnityEngine;
using System.Linq;
using Kyzlyk.Helpers.Math;
using Kyzlyk.LSGSystem.Breaking;
using System.Collections.Generic;

namespace Kyzlyk.Helpers.Extensions
{
    public static class Extensions
    {
        public static Vector2 Ceil(this Vector2 vector2)
            => new(Mathf.Ceil(vector2.x), Mathf.Ceil(vector2.y));

        public static Vector2Int ToVector2Int(this Vector2 vector2)
            => new(Mathf.FloorToInt(vector2.x), Mathf.FloorToInt(vector2.y));

        public static Vector2 Floor(this Vector2 vector2)
            => new(Mathf.Floor(vector2.x), Mathf.Floor(vector2.y));
        
        public static T Random<T>(this IReadOnlyList<T> array)
        {
            if (array.Count == 0)
                return default;

            return array[UnityEngine.Random.Range(0, array.Count)];
        }

        public static Vector3 ToVector3(this Vector2 vector2, float z)
            => new(vector2.x, vector2.y, z);

        public static Vector3 Abs(this Vector3 vector3)
            => new(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y));

        public static Vector2 ToVector2(this float f)
            => new(f, f);
        
        public static Vector2Int ToVector2Int(this int i)
            => new(i, i);

        public static bool Compare(this Vector2 vector2, Vector2 toCompare)
        {
            return Mathf.Approximately(vector2.x, toCompare.x) && Mathf.Approximately(vector2.y, toCompare.y);
        }

        public static Vector2 Round(this Vector2 vector2)
        {
            return new Vector2(Mathf.Round(vector2.x), Mathf.Round(vector2.y));
        }

        public static bool IsOdd(this int i)
            => i % 2 == 0;

        public static bool IsNullOrEmpty<T>(this T[] array) => array == null || array.Length == 0;

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action(array[i]);
            }
        }
        
        public static void ForEach<T>(this T[] array, Action<T, int> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action(array[i], i);
            }
        }
        
        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action)
        {
            foreach (var item in array)
            {
                action?.Invoke(item);
            }
        }

        public static int RandomRange(int minInclusive, int maxInclusive, params int[] exclusions)
        {
            int value;
            int repeated = 0;

            do
            {
                value = UnityEngine.Random.Range(minInclusive, maxInclusive + 1);

                if (repeated >= exclusions.Length)
                    throw new Exception("The parameter 'exclusion' contains all numbers in range between 'minInclusive' and 'maxInclusive'!");

                repeated++;
            }
            while (exclusions.Contains(value));

            return value;
        }

        public static T[] RandomizeArray<T>(this T[] arr, int leave)
        {
            int[] randomIndexes = new int[leave];

            for (int i = 0; i < leave; i++)
            {
                randomIndexes[i] = RandomRange(0, arr.Length - 1, randomIndexes);
            }

            T[] randomizedArray = new T[randomIndexes.Length];

            for (int i = 0; i < randomIndexes.Length; i++)
            {
                randomizedArray[i] = arr[randomIndexes[i]];
            }
            
            return randomizedArray;
        }

        public static (bool, Vector2) CheckWay(this Builder builder, Vector2 from, Vector2 to)
        {
            float distance = Vector2.Distance(from, to);

            Vector2 direction = MathUtility.GetVector(from, to);
            Vector2 nextPosition = new();

            for (int i = 0; i < distance; i++)
            {
                nextPosition = (from + new Vector2(i, i) * (direction / distance)).Floor();

                if (builder.HasGMaterial(nextPosition))
                {
                    return (true, nextPosition);
                }
            }

            return (false, nextPosition);
        }

        public static void Shuffle<T>(this T[] arr)
        {
            System.Random rnd = new();

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                int j = rnd.Next(i + 1);

                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
        
        public static void Shuffle<T>(this IList<T> arr)
        {
            System.Random rnd = new();

            for (int i = arr.Count - 1; i >= 0; i--)
            {
                int j = rnd.Next(i + 1);

                (arr[j], arr[i]) = (arr[i], arr[j]);
            }
        }

        public static T[] GetShuffledArray<T>(this T[] arr)
        {
            System.Random rnd = new();

            T[] newArr = new T[arr.Length];

            arr.CopyTo(new Memory<T>(newArr));

            for (int i = newArr.Length - 1; i >= 0; i--)
            {
                int j = rnd.Next(i + 1);

                T temp = newArr[j];

                newArr[i] = newArr[j];
                newArr[j] = temp;
            }

            return newArr;
        }

        public static T[] Remove<T>(this T[] arr, T value)
        {
            T[] newArray = new T[arr.Length - 1];

            for (int i = 0, j = 0; i < arr.Length; i++, j++)
            {
                if (arr[i].Equals(value))
                    continue;

                newArray[j] = arr[i];
            }

            return newArray;
        }

        public static T[] Extract<T>(this T[] arr, int startIndexInclusive, int endIndexInclusive)
        {
            T[] cutedArray = new T[endIndexInclusive - startIndexInclusive + 1];

            for (int i = startIndexInclusive, j = 0; i <= endIndexInclusive; i++, j++)
            {
                cutedArray[j] = arr[i];
            }

            return cutedArray;
        }
        
        public static T[] Cut<T>(this T[] arr, int startIndexInclusive, int endIndexInclusive)
        {
            T[] cutedArray = new T[endIndexInclusive - startIndexInclusive + 1];

            for (int i = 0, j = 0; i < arr.Length; i++)
            {
                if (i >= startIndexInclusive && i <= endIndexInclusive)
                    continue;

                cutedArray[j] = arr[i];
                j++;
            }

            return cutedArray;
        }
        
        public static T[] Cut<T>(this T[] arr, int index)
        {
            return arr.Cut(index, index);
        }
        
        public static IEnumerable<T> Extract<T>(this IEnumerable<T> arr, int startIndexInclusive, int endIndexInclusive)
        {
            return arr.ToArray().Extract<T>(startIndexInclusive, endIndexInclusive);
        }
        
        public static IEnumerable<T> Cut<T>(this IEnumerable<T> arr, int startIndexInclusive, int endIndexInclusive)
        {
            return arr.ToArray().Cut(startIndexInclusive, endIndexInclusive);
        }
        
        public static IEnumerable<T> Cut<T>(this IEnumerable<T> arr, int index)
        {
            return arr.ToArray().Cut(index);
        }

        public static bool IsObjectVisibleByCamera(this Camera camera, Vector3 objectPosition)
        {
            Vector3 viewPos = camera.WorldToViewportPoint(objectPosition);

            return viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0;
        }

        public static TObject[] GetObjectsFromArray<TObject, TArray>(this TArray[] array, Func<TArray, TObject> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            else if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            TObject[] objects = new TObject[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                TObject obj = selector(array[i]);
                if (obj == null)
                    continue;
                else
                    objects[i] = obj;
            }

            return objects;
        }

        public static IList<TObject> GetObjectsFromArray<TObject, TArray>(this IList<TArray> array, Func<TArray, TObject> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            else if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            List<TObject> objects = new(array.Count);

            for (int i = 0; i < array.Count; i++)
            {
                TObject obj = selector(array[i]);
                if (obj == null)
                    continue;
                else
                    objects.Add(obj);
            }

            return objects;
        }

        public static IEnumerable<TObject> GetObjectsFromArray<TObject, TArray>(this IEnumerable<TArray> array, Func<TArray, TObject> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            else if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            foreach (var item in array)
            {
                TObject obj = selector(item);

                if (obj == null)
                    continue;
                else
                    yield return obj;
            }
        }
    }
}