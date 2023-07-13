using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Kyzlyk.Helpers.Utils
{
    public struct UnityUtility
    {
        public static T[] GetAllInheritedObjects<T>(params object[] constructorArguments)
        {
            List<T> implementors = new();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            var types = executingAssembly.GetTypes().Where(t => typeof(T).IsAssignableFrom(t));

            foreach (var type in types)
            {
                if (!type.IsInterface && !type.IsAbstract)
                {
                    implementors.Add((T)Activator.CreateInstance(type, constructorArguments));
                }
            }

            return implementors.ToArray();
        }

        public static Type GetUnityType(string typeName)
        {
            Type type = Type.GetType(typeName);

            if (type != null)
                return type;

            if (typeName.Contains("."))
            {
                string assemblyName = typeName[..typeName.IndexOf('.')];

                Assembly assembly = Assembly.Load(assemblyName);
                if (assembly == null)
                    return null;

                type = assembly.GetType(typeName);
                if (type != null)
                    return type;
            }

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            AssemblyName[] referencedAssemblies = currentAssembly.GetReferencedAssemblies();

            foreach (var assemblyName in referencedAssemblies)
            {
                Assembly assembly = Assembly.Load(assemblyName);
                if (assembly != null)
                {
                    type = assembly.GetType(typeName);
                    if (type != null)
                        return type;
                }
            }

            return null;
        }

        public static T[] GetAllInheritedObjects<T>(Predicate<Type> selector, params object[] constructorArguments)
        {
            List<T> implementors = new();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            var types = executingAssembly.GetTypes().Where(t => typeof(T).IsAssignableFrom(t));

            foreach (var type in types)
            {
                if (!type.IsInterface && !type.IsAbstract)
                {
                    if (selector(type))
                        implementors.Add((T)Activator.CreateInstance(type, constructorArguments));
                }
            }

            return implementors.ToArray();
        }

        public static bool IsLayerIncluded(LayerMask layerMask, int layer)
        {
            int shiftedMask = 1 << layer;

            return (layerMask & shiftedMask) != 0;
        }

        public static Sprite ConvertTexture2DToSprite(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0f, 0f));
        }

        public static IList<T> GetAllObjectsOnScene<T>(bool includeInactive = true) where T : class
        {
            List<T> values = new List<T>();

            MonoBehaviour[] scripts = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>(includeInactive);
            for (int i = 0; i < scripts.Length; i++)
            {
                if (scripts[i] is T script)
                    values.Add(script);
            }

            return values;
        }

        public static Color GetRandomColor()
        {
            static float Random() => UnityEngine.Random.Range(0f, 1f);
            
            return new Color(Random(), Random(), Random());
        }

        public static void ClearDebugConsole()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            Type type = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
    }
}