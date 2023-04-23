using System;
using UnityEngine;
using System.Collections.Generic;

namespace Kyzlyyk.Utils
{
    public static class Extensions
    {
        public static Vector2Int ToVector2Int(this Vector2 vector2)
            => new(Mathf.FloorToInt(vector2.x), Mathf.FloorToInt(vector2.y));

        public static Vector2 Floor(this Vector2 vector2)
            => new(Mathf.Floor(vector2.x), Mathf.Floor(vector2.y));
        
        public static T Random<T>(this IReadOnlyList<T> array) 
            => array[UnityEngine.Random.Range(0, array.Count)];

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action?.Invoke(array[i]);
            }
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
    }
}