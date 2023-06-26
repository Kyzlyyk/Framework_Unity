using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public abstract class Shape2D : IEnumerator<Vector2>
    {
        public abstract Vector2 Size { get; }
        public abstract Quaternion Rotation { get; }
        public abstract Vector2 Center { get; }
        public abstract Vector2[] Points { get; }

        public abstract float GetPerimeter();
        public abstract float GetArea();

        public Vector2 Current => Points[_currentIndex];

        private int _currentIndex;

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_currentIndex >= Points.Length)
                return false;

            _currentIndex++;
            return true;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }

        public void Dispose()
        {
        }
    }

    public enum Shape2DType
    {
        Triangle = 0,
        Square = 1,
        Rectangle = 2,
        Circle = 3,
        Elipse = 4,
    }
}
