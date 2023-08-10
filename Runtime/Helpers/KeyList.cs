using System.Collections;
using System.Collections.Generic;

namespace Kyzlyk.Helpers
{
    public class KeyList<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
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

        private readonly List<TKey> _keys;
        private readonly List<TValue> _values;

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

        public void Clear()
        {
            _keys.Clear();
            _values.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => new Enumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            public Enumerator(KeyList<TKey, TValue> keyList)
            {
                if (keyList.Count == 0)
                    Current = default;
                else
                    Current = new KeyValuePair<TKey, TValue>(keyList._keys[0], keyList._values[0]);

                _keyList = keyList;
                _index = 0;
            }

            public KeyValuePair<TKey, TValue> Current { get; private set; }
            private int _index;
            private readonly KeyList<TKey, TValue> _keyList;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_index >= _keyList.Count)
                    return false;

                Current = GetPairFromList(_index);
                _index++;

                return true;
            }

            private KeyValuePair<TKey, TValue> GetPairFromList(int index)
            {
                return new KeyValuePair<TKey, TValue>(_keyList._keys[index], _keyList._values[index]);
            }

            public void Reset()
            {
                _index = 0;
            }
        }
    }
}