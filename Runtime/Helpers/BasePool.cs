using System;
using System.Collections.Generic;

namespace Kyzlyk.Helpers
{
    public class BasePool<T>
    {
        private Queue<T> _pool;
        private List<T> _active;

        private readonly Func<T> _preload;
        private readonly Action<T> _returnAction;
        private readonly Action<T> _getAction;

        public BasePool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
        {
            _preload = preloadFunc;
            _returnAction = returnAction;
            _getAction = getAction;

            if (_preload == null)
            {
                UnityEngine.Debug.LogError("PreloadFunc is null!");
                return;
            }

            _pool = new Queue<T>(preloadCount);
            _active = new List<T>(preloadCount);

            for (int i = 0; i < preloadCount; i++)
                Return(_preload());
        }

        public T Get()
        {
            T item = _pool.Count > 0 ? _pool.Dequeue() : _preload();
            
            _getAction(item);
            _active.Add(item);

            return item;
        }

        public void Return(T item)
        {
            _returnAction(item);
            _pool.Enqueue(item);
            _active.Remove(item);
        }

        public void ReturnAll()
        {
            for (int i = 0; i < _active.Count; i++)
                Return(_active[i]);
        }
    }
}
