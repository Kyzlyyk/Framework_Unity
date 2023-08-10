using Kyzlyk.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kyzlyk.Core.Builders
{
    public sealed class ArrayBuilder<T>
    {
        public ArrayBuilder(int capacity)
        {
            _array = new T[capacity];
        }

        private readonly T[] _array;
        private int _lastIndex;

        public ArrayBuilder<T> Append(T[] array)
        {
            Array.Copy(array, 0, _array, _lastIndex, array.Length);
            _lastIndex += array.Length;
            return this;
        }
        
        public ArrayBuilder<T> Append(IEnumerable<T> array)
        {
            foreach (var item in array)
            {
                _array[_lastIndex++] = item;
            }
            
            return this;
        }
        
        public ArrayBuilder<T> Append<T1>(IEnumerable<T1> array) where T1 : T
        {
            foreach (var item in array)
            {
                _array[_lastIndex++] = item;
            }
            
            return this;
        }
        
        public ArrayBuilder<T> Append<T1>(T1[] array) where T1 : T
        {
            for (int i = 0; i < array.Length; i++)
            {
                _array[_lastIndex + i] = array[i];
            }
            
            _lastIndex += array.Length;
            return this;
        }
        
        public ArrayBuilder<T> Append(int startIndex, int length, T[] array)
        {
            if (length > array.Length)
                ComparisonException.Throw(nameof(length), $"{array}.{array.Length}", ComparisonType.LessThanOrEqual);

            if (startIndex < 0)
                ComparisonException.Throw(nameof(startIndex), 0, ComparisonType.GreaterThanOrEqual);

            Array.Copy(array, startIndex, _array, _lastIndex, length);
            _lastIndex += length;
            return this;
        }
        
        public ArrayBuilder<T> Append(T value)
        {
            _array[_lastIndex++] = value;
            return this;
        }

        public T[] ToArray() => _array;
    }
}
