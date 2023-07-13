using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kyzlyk.Helpers.Utils
{
    public struct GUtility
    {
        public delegate (int Left, int Right) Converter<T>(T left, T right);

        public static void BubbleSort(int[] array)
        {
            int n = array.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;

                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }
        
        public static void BubbleSort<T>(T[] array, Converter<T> converter)
        {
            int n = array.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;

                for (int j = 0; j < n - i - 1; j++)
                {
                    (int left, int right) = converter(array[j], array[j + 1]);

                    if (left > right)
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }

        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        private static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(arr, left, right);
                QuickSort(arr, left, pivotIndex - 1);
                QuickSort(arr, pivotIndex + 1, right);
            }
        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, right);
            return i + 1;
        }

        private static void Swap(int[] arr, int i, int j)
        {
            (arr[j], arr[i]) = (arr[i], arr[j]);
        }

        public static string HandleFieldName(string fieldName, char separator = ' ', bool upperBeforeSeparator = true, params char[] exclude)
        {
            if (string.IsNullOrEmpty(fieldName))
                return string.Empty;

            StringBuilder stringBuilder = new();

            char previousChar = '\0';

            for (int i = 0; i < fieldName.Length; i++)
            {
                if (exclude.Contains(fieldName[i]))
                {
                    if (previousChar != '\0')
                    {
                        stringBuilder.Append(separator);
                        previousChar = separator;
                    }

                    continue;
                }

                if (upperBeforeSeparator && (previousChar == separator || previousChar == '\0'))
                    stringBuilder.Append(char.ToUpper(fieldName[i]));

                else if (char.IsLower(previousChar) && char.IsUpper(fieldName[i]))
                {
                    stringBuilder.Append(separator);
                    stringBuilder.Append(upperBeforeSeparator ? fieldName[i] : char.ToLower(fieldName[i]));
                }

                else
                    stringBuilder.Append(fieldName[i]);

                previousChar = fieldName[i];
            }

            return stringBuilder.ToString();
        }

        public static IEnumerable<T> GetAllAttributes<T>() where T : Attribute
        {
            return Assembly.GetExecutingAssembly().GetCustomAttributes<T>();
        }

        public static IEnumerable<FieldInfo> GetFieldsWithAttribute<TAttribute>(bool attributeInheriting = false, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static) where TAttribute : Attribute
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type attributeType = typeof(TAttribute);

            return assembly.GetTypes()
                .SelectMany(type => type.GetFields(bindingFlags))
                .Where(field => field.IsDefined(attributeType, attributeInheriting));
        }

        public static IEnumerable<FieldInfo> GetFieldsWithAttribute<TAttribute>(Type specifically, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static) where TAttribute : Attribute
        {
            FieldInfo[] fields = specifically.GetFields(bindingFlags);

            foreach (FieldInfo field in fields)
            {
                if (Attribute.GetCustomAttribute(field, typeof(TAttribute)) is TAttribute)
                {
                    yield return field;
                }
            }
        }

        /// <summary>
        /// Except for System.Object
        /// </summary>
        /// <param name="derivedType"></param>
        /// <returns></returns>
        public static Type GetFinalBaseType(Type derivedType)
        {
            Type baseType = derivedType;

            while (baseType.BaseType != null && baseType.BaseType != typeof(object))
            {
                baseType = baseType.BaseType;
            }

            return baseType;
        }

        public static Type GetFinalBaseType(Type derivedType, Type final)
        {
            Type baseType = derivedType;

            while (baseType.BaseType != null)
            {
                baseType = baseType.BaseType;

                if (baseType.BaseType == final)
                    return baseType;
            }

            return baseType;
        }

        public static int ConvertStringToInt(string str)
        {
            int result = 0;

            for (int i = 0; i < str.Length; i++)
            {
                result += str[i];
            }

            return result;
        }

        public static int BoolToSign(bool b) => b ? 1 : -1;
        public static float BoolToSignF(bool b) => b ? 1f : -1f;
    }
}