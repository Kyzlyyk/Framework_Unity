using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Helpers.GMesh;

namespace Kyzlyk.Helpers.Utils
{
    public struct UnityUtils
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
    }
}