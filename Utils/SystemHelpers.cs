using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Kyzlyyk.Utils
{
    public struct SystemHelpers
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
    }
}