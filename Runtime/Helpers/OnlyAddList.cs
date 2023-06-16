using System.Collections;
using System.Collections.Generic;

namespace Kyzlyk.Helpers
{
    public interface IAdder<T> : IEnumerable<T>
    {
        void Add(T value);
    }

    public class OnlyAddList<T> : IAdder<T>
    {
        private readonly List<T> _items = new();

        public void Add(T value)
        {
            _items.Add(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
