using System.Collections.Generic;

namespace Kyzlyyk.Utils
{
    public class KeyList<TKey, TValue>
    {
        public KeyList(int capacity)
        {
            Keys = new(capacity);
            Values = new(capacity);
        }

        public int Count => _values.Count;

        public TValue this[int index]
        {
            get => _values[index];
            set => _values[index] = value;
        }

        public List<TKey> Keys { get; private set; }
        public List<TValue> Values { get; private set; }

        public void Add(TKey key, TValue value)
        {
            Keys.Add(key);
            Values.Add(value);

            Count++;
        }

        public void Remove(TKey key)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Keys[i].Equals(key))
                {
                    Values.RemoveAt(i);
                    Count--;
                }
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Keys[i].Equals(key))
                {
                    value = Values[i];
                    return true;
                }
            }

            value = default;
            return false;
        }

        public bool Contains(TKey key)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Keys[i].Equals(key))
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