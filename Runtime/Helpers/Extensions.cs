using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Kyzlyk.LSGSystem.Breaking;

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
            => array[UnityEngine.Random.Range(0, array.Count)];

        public static Vector3 ToVector3(this Vector2 vector2, float z)
            => new(vector2.x, vector2.y, z);

        public static Vector3 Abs(this Vector3 vector3)
            => new(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y));


        public static Vector2 ToVector2(this float f)
            => new(f, f);

        public static bool Compare(this Vector2 vector2, Vector2 toCompare)
        {
            return Mathf.Approximately(vector2.x, toCompare.x) && Mathf.Approximately(vector2.y, toCompare.y);
        }

        public static Vector2 Round(this Vector2 vector2)
        {
            return new Vector2(Mathf.Round(vector2.x), Mathf.Round(vector2.y));
        }

        /// <summary>
        /// return Mathf.Abs(f) / Mathf.Max(f, 1)
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static float GetDirection(this float f)
            => Mathf.Abs(f) / Mathf.Max(f, 1);

        /// <summary>
        /// return Mathf.Abs(i) / Mathf.Max(i, 1)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int GetDirection(this int i)
            => Mathf.Abs(i) / Mathf.Max(i, 1);

        public static bool IsOdd(this int i)
            => i % 2 == 0;

        public static bool IsNullOrEmpty<T>(this T[] array) => array == null || array.Length == 0;

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action?.Invoke(array[i]);
            }
        }
        
        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action)
        {
            foreach (var item in array)
            {
                action?.Invoke(item);
            }
        }

        public static (bool, Vector2) CheckWay(this Builder builder, Vector2 from, Vector2 to)
        {
            float distance = Vector2.Distance(from, to);

            Vector2 direction = Utils.MathUtility.GetVector(from, to);

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

                T temp = arr[j];

                arr[i] = arr[j];
                arr[j] = temp;
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

        public static T[] Cut<T>(this T[] arr, int startIndexInclusive, int endIndexInclusive)
        {
            T[] cutedArray = new T[startIndexInclusive + endIndexInclusive];

            for (int i = startIndexInclusive; i <= endIndexInclusive; i++)
            {
                cutedArray[i] = arr[i];
            }

            return cutedArray;
        }

        public static bool IsObjectVisibleByCamera(this Camera camera, Vector3 objectPosition)
        {
            Vector3 viewPos = camera.WorldToViewportPoint(objectPosition);

            return viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0;
        }

        public static TObject[] GetObjectsFromArray<TObject, TArray>(this TArray[] array, Func<TArray, TObject> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            else if (match == null)
                throw new ArgumentNullException(nameof(match));

            TObject[] objects = new TObject[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                TObject obj = match(array[i]);
                if (obj == null)
                    continue;
                else
                    objects[i] = obj;
            }

            return objects;
        }

        public static IList<TObject> GetObjectsFromArray<TObject, TArray>(this IList<TArray> array, Func<TArray, TObject> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            else if (match == null)
                throw new ArgumentNullException(nameof(match));

            List<TObject> objects = new(array.Count);

            for (int i = 0; i < array.Count; i++)
            {
                TObject obj = match(array[i]);
                if (obj == null)
                    continue;
                else
                    objects.Add(obj);
            }

            return objects;
        }

        public static IEnumerable<TObject> GetObjectsFromArray<TObject, TArray>(this IEnumerable<TArray> array, Func<TArray, TObject> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            else if (match == null)
                throw new ArgumentNullException(nameof(match));

            foreach (var item in array)
            {
                TObject obj = match(item);

                if (obj == null)
                    continue;
                else
                    yield return obj;
            }
        }
    }
}