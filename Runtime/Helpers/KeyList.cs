using System.Collections.Generic;

namespace Kyzlyk.Helpers
{
    public class KeyList<TKey, TValue>
    {
        public KeyList(int capacity)
        {
            _keys = new(capacity);
            _values = new(capacity);
        }

        public int Count => _values.Count;

        public TValue this[int index]
        {
            get => _values[index];
            set => _values[index] = value;
        }

        private List<TKey> _keys;
        private List<TValue> _values;

        public void Add(TKey key, TValue value)
        {
            _keys.Add(key);
            _values.Add(value);
        }

        public void Remove(TKey key)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_keys[i].Equals(key))
                    _values.RemoveAt(i);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_keys[i].Equals(key))
                {
                    value = _values[i];
                    return true;
                }
            }

            value = default;
            return false;
        }

        public TValue GetValue(TKey key)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_keys[i].Equals(key))
                {
                    return _values[i];
                }
            }

            return default;
        }

        public bool Contains(TKey key)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_keys[i].Equals(key))
                {
                    return true;
                }
            }

            return false;
        }
        
        public bool Contains(TKey key, out int index)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_keys[i].Equals(key))
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }
        
        public bool Contains(TKey key, out TValue value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_keys[i].Equals(key))
                {
                    value = this[i];
                    return true;
                }
            }
            value = default;
            return false;
        }

        public TKey GetKey(int index)
        {
            return _keys[index];
        }
    }
}