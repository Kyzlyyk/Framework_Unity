using System;
using UnityEngine;
using System.Linq;
using ue = UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Kyzlyk.Core;
using Kyzlyk.Helpers.Math;
using System.Text;

namespace Kyzlyk.Helpers.Extensions
{
    public static class Extensions
    {
        public static Vector2 Ceil(this Vector2 vector2)
            => new(Mathf.Ceil(vector2.x), Mathf.Ceil(vector2.y));

        public static Vector2 Floor(this Vector2 vector2)
            => new(Mathf.Floor(vector2.x), Mathf.Floor(vector2.y));

        public static Vector2 Round(this Vector2 vector2)
            => new(Mathf.Round(vector2.x), Mathf.Round(vector2.y));

        public static float ToSize(this Vector2 vector2)
            => vector2.x * vector2.y;

        public static Vector2Int RoundToInt(this Vector3 vector3)
            => new(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));

        public static Vector3 ToVector3(this Vector2Int vector2Int, float z = 0f)
            => new(vector2Int.x, vector2Int.y, z);

        public static int Perimeter(this Vector2Int vector2Int)
            => vector2Int.x * vector2Int.y;

        public static bool IsZero(this Vector2 vector2)
            => vector2.x == 0 && vector2.y == 0;

        public static bool IsAnyNull<T>(this IEnumerable<T> array) where T : class
        {
            foreach (var item in array)
            {
                if (item == null)
                    return true;
            }

            return false;
        }

        public static T Random<T>(this IReadOnlyList<T> array)
        {
            if (array.Count == 0)
                return default;

            return array[ue::Random.Range(0, array.Count)];
        }

        public static bool Random<T>(this IReadOnlyList<T> array, out T value)
        {
            if (array.Count == 0)
            {
                value = default;
                return false;
            }

            value = array[ue::Random.Range(0, array.Count)];
            return true;
        }

        public static Vector3 ToVector3(this Vector2 vector2, float z) => new(vector2.x, vector2.y, z);
        public static Vector3 Abs(this Vector3 vector3) => new(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y));
        public static Vector2 ToVector2(this float f) => new(f, f);
        public static Vector2Int ToVector2Int(this int i) => new(i, i);

        public static bool Compare(this Vector2 vector2, Vector2 other)
        {
            return Mathf.Approximately(vector2.x, other.x) && Mathf.Approximately(vector2.y, other.y);
        }

        public static bool Compare(this Vector3 vector3, Vector3 other)
        {
            return Mathf.Approximately(vector3.x, other.x) && Mathf.Approximately(vector3.y, other.y) && Mathf.Approximately(vector3.z, other.z);
        }

        public static bool Compare(this Vector2 vector2, Vector2 other, float tolerance)
        {
            tolerance = Mathf.Abs(tolerance);
            return (Mathf.Abs(vector2.x - other.x) <= tolerance) && (Mathf.Abs(vector2.y - other.y) <= tolerance);
        }

        public static bool Compare(this Vector3 vector3, Vector3 other, float tolerance)
        {
            tolerance = Mathf.Abs(tolerance);
            return (Mathf.Abs(vector3.x - other.x) <= tolerance) && (Mathf.Abs(vector3.y - other.y) <= tolerance) && (Mathf.Abs(vector3.z - other.z) <= tolerance);
        }

        public static bool Compare(this float f, float other, float tolerance)
        {
            return Mathf.Abs(f - other) <= Mathf.Abs(tolerance);
        }

        public static bool IsEven(this int i) => i % 2 == 0;
        public static bool IsOdd(this int i) => i % 2 == 1;

        public static bool IsNullOrEmpty<T>(this T[] array) => array == null || array.Length == 0;

        public static void ForEachAsSpan<T>(this List<T> list, Action<T> action)
        {
            Span<T> span = list is null ? default : new Span<T>(list.ToArray(), 0, list.Count);
            for (int i = 0; i < span.Length; i++)
            {
                action(span[i]);
            }
        }

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

        public static List<T> Where_List<T>(this T[] array, Func<T, bool> func)
        {
            List<T> list = new List<T>(array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                if (func(array[i]))
                    list.Add(array[i]);
            }

            return list;
        }

        public static int RandomRange(int minInclusive, int maxInclusive, bool throwException, params int[] exclusions)
        {
            int value;
            int repeated = 0;

            do
            {
                value = ue::Random.Range(minInclusive, maxInclusive + 1);

                if (repeated >= exclusions.Length)
                {
                    if (throwException)
                        throw new Exception("The parameter 'exclusion' contains all numbers in range between 'minInclusive' and 'maxInclusive'!");
                    else
                        return 0;
                }

                repeated++;
            }
            while (exclusions.Contains(value));

            return value;
        }

        public static T[] RandomizeArray<T>(this T[] arr, int leave, bool throwException = true)
        {
            if (leave > arr.Length || leave < 0)
            {
                if (throwException)
                    throw new Exception("Leave is grater than 'array' size or less than 0!");

                return arr;
            }

            int[] randomIndexes = new int[leave];

            for (int i = 0; i < leave; i++)
            {
                randomIndexes[i] = RandomRange(0, arr.Length - 1, false, randomIndexes);
            }

            T[] randomizedArray = new T[randomIndexes.Length];

            for (int i = 0; i < randomIndexes.Length; i++)
            {
                randomizedArray[i] = arr[randomIndexes[i]];
            }

            return randomizedArray;
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

        public static T[] Remove<T>(this T[] array, T value)
        {
            int index = Array.IndexOf(array, value);
            if (index == -1)
            {
                return array;
            }

            T[] newArray = new T[array.Length - 1];

            Array.Copy(array, 0, newArray, 0, index);
            Array.Copy(array, index + 1, newArray, index, array.Length - index - 1);

            return newArray;
        }

        public static T[] RemoveLast<T>(this T[] array)
        {
            T[] newArray = new T[array.Length - 1];
            Array.Copy(array, newArray, array.Length - 1);

            return newArray;
        }

        public static T[] AddLast<T>(this T[] array, T value, ArrayActionToHandleNull actionIfNull = ArrayActionToHandleNull.ThrowException)
        {
            if (array == null)
            {
                if (actionIfNull == ArrayActionToHandleNull.CreateNew)
                    return new T[1] { value };

                else if (actionIfNull == ArrayActionToHandleNull.ThrowException)
                    throw new ArgumentNullException("The array is null to add something!");
            }

            T[] newArray = new T[array.Length + 1];
            Array.Copy(array, newArray, array.Length);
            newArray[^1] = value;

            return newArray;
        }

        public static T[] Cut<T>(this T[] arr, int index)
        {
            return arr.Cut(index, index);
        }

        public static T[] Cut<T>(this T[] array, int startIndexInclusive, int endIndexInclusive)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (startIndexInclusive < 0 || startIndexInclusive >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndexInclusive), "Start index is out of range.");

            if (endIndexInclusive < 0 || endIndexInclusive >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(endIndexInclusive), "End index is out of range.");

            if (startIndexInclusive > endIndexInclusive)
                throw new ArgumentException("Start index cannot be greater than end index.");

            int length = endIndexInclusive - startIndexInclusive + 1;
            T[] result = new T[length];

            Array.Copy(array, startIndexInclusive, result, 0, length);

            return result;
        }

        public static IEnumerable<T> Cut<T>(this IEnumerable<T> arr, int startIndexInclusive, int endIndexInclusive)
        {
            return arr.ToArray().Cut(startIndexInclusive, endIndexInclusive);
        }

        public static IEnumerable<T> Cut<T>(this IEnumerable<T> arr, int index)
        {
            return arr.ToArray().Cut(index);
        }

        public static bool IsObjectVisible(this Camera camera, Vector3 objectPosition)
        {
            Vector3 viewPos = camera.WorldToViewportPoint(objectPosition);

            return viewPos.x >= 0f && viewPos.x <= 1f && viewPos.y >= 0f && viewPos.y <= 1f && viewPos.z > 0f;
        }
        
        public static bool IsObjectVisible(this Camera camera, Bounds bounds)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), bounds);
        }
        
        public static T Reduce<T, TElement>(this TElement[] array, Func<TElement, T, T> match, T obj)
        {
            for (int i = 0; i < array.Length; i++)
            {
                obj = match(array[i], obj);
            }

            return obj;
        }
        
        public static T Reduce<T, TElement>(this IEnumerable<TElement> array, Func<TElement, T, T> match, T obj)
        {
            foreach (var item in array)
            {
                obj = match(item, obj);
            }

            return obj;
        }

        public static Vector2 ScreenToWorldPoint2D(this Camera camera, Vector3 point)
        {
            point.z = Mathf.Abs(camera.transform.position.z);
            return camera.ScreenToWorldPoint(point);
        }

        public static Vector2 ScreenToWorldPoint3D(this Camera camera, Vector3 point)
        {
            return camera.ScreenPointToRay(point).GetPoint(Mathf.Abs(camera.transform.position.z));
        }

        public static void Shake(this Camera camera, float duration, float magnitude, ICoroutineExecutor coroutineExecutor)
        {
            Vector3 origin = camera.transform.localPosition;

            IEnumerator Shake_Coroutine()
            {
                float elapsedTime = 0f;

                while (elapsedTime < duration)
                {
                    float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
                    float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

                    camera.transform.localPosition = origin + new Vector3(x, y);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                camera.transform.localPosition = origin;
            }

            coroutineExecutor.StartCoroutine(Shake_Coroutine());
        }

        public static (Segment Bottom, Segment Right, Segment Up, Segment Left) GetEdges(this Bounds bounds)
        {
            Vector2[] points = GetEdgePoints(bounds);
            
            return (new Segment(points[0], points[1]), 
                new Segment(points[1], points[2]), 
                new Segment(points[2], points[3]), 
                new Segment(points[3], points[0]));
        }
        
        public static Vector2[] GetEdgePoints(this Bounds bounds)
        {
            float halfWidth = bounds.size.x * .5f;
            float halfHeight = bounds.size.y * .5f;

            Vector2 bottomLeft = new(bounds.center.x - halfWidth, bounds.center.y - halfHeight);
            Vector2 bottomRight = new(bounds.center.x + halfWidth, bounds.center.y - halfHeight);
            Vector2 upRight = new(bottomRight.x, bounds.center.y + halfHeight);
            Vector2 upLeft = new(bottomLeft.x, upRight.y);

            return new Vector2[4]
            {
                bottomLeft,
                bottomRight,
                upRight,
                upLeft
            };
        }
                
        public static void GetEdges(this Bounds bounds, out Vector2[] result)
        {
            float halfWidth = bounds.size.x * .5f;
            float halfHeight = bounds.size.y * .5f;

            Vector2 bottomLeft = new(bounds.center.x - halfWidth, bounds.center.y - halfHeight);
            Vector2 bottomRight = new(bounds.center.x + halfWidth, bounds.center.y - halfHeight);
            Vector2 upRight = new(bottomRight.x, bounds.center.y + halfHeight);
            Vector2 upLeft = new(bottomLeft.x, upRight.y);

            result = new Vector2[4]
            {
                bottomLeft, bottomRight, upRight, upLeft
            };
        }

        public static void OverlapBounds2D(this Bounds bounds, Action<Vector2> action)
        {
            Vector2 startPoint = bounds.center.RoundToInt() - ((Vector2)bounds.size * .5f);
            Vector2 current = startPoint;

            int sizeX = Mathf.RoundToInt(bounds.size.x);
            int sizeY = Mathf.RoundToInt(bounds.size.y);
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    action(new Vector2(j, i) + (Vector2)bounds.center);
                    current.x++;
                }
                
                current.x = startPoint.x;
                current.y++;
            }
        }

        public static Vector3 GetSize(this Camera camera)
        {
            float height = camera.orthographicSize * 2f;
            return new Vector3(height * camera.aspect, height, camera.depth);
        }

        public static string FirstToLower(this string text)
        {
            StringBuilder stringBuilder = new(text.Length);
            
            return stringBuilder
                .Append(char.ToLower(text[0]))
                .Append(text[1..])
                .ToString();
        }

        public static T TryAddComponent<T>(this Component target) where T : Component
        {
            return target.gameObject.TryAddComponent<T>();
        }
        
        public static T TryAddComponent<T>(this GameObject target) where T : Component
        {
            if (target.TryGetComponent<T>(out var component))
                return component;
            else
                return target.AddComponent<T>();
        }
    }

    public enum ArrayActionToHandleNull
    {
        ThrowException,
        CreateNew
    }
}