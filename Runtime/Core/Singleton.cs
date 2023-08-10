using UnityEngine;

namespace Kyzlyk.Core
{
    public abstract class Singleton<T> : Singleton where T : MonoBehaviour
    {
        private static T s_instance;

        private static readonly object s_lock = new();

        protected virtual bool IsPersistance { get => false; }

        public static T Instance
        {
            get
            {
                if (Quiting)
                {
                    Debug.LogWarning($"{GetName()} Instance will not be returned because application quiting!");
                    return null;
                }
                lock (s_lock)
                {
                    if (s_instance != null)
                        return s_instance;

                    T[] instances = FindObjectsOfType<T>();

                    if (instances.Length == 1)
                        return s_instance = instances[0];

                    if (instances.Length > 1)
                    {
                        Debug.LogWarning($"{GetName()} There should never be more than one instance!");
                        for (int i = 1; i < instances.Length; i++)
                            Destroy(instances[i]);

                        return s_instance = instances[0];
                    }

                    Debug.LogWarning($"{GetName()} The scene requires an instance, but it does not exist!");
                    return s_instance = new GameObject($"({nameof(Singleton)}){typeof(T)}").AddComponent<T>();
                }
            }
        }

        private static string GetName()
            => $"[{nameof(Singleton)}<{typeof(T)}>]";

        private void Awake()
        {
            if (IsPersistance)
                DontDestroyOnLoad(gameObject);

            OnAwake();
        }

        protected virtual void OnAwake() { }
    }

    public abstract class Singleton : MonoBehaviour
    {
        public static bool Quiting { get; private set; }

        private void OnApplicationQuit()
        {
            Quiting = true;
        }
    }
}
