using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public abstract class Shape2D : IEnumerator<Vector2>
    {
        public abstract Vector2 Size { get; }
        public abstract Vector3 Rotation { get; }
        public abstract Vector2 Center { get; }
        public abstract Vector2[] Points { get; }

        public abstract float GetPerimeter();
        public abstract float GetArea();

        public Vector2 Current => Points[CurrentIndex];

        protected int CurrentIndex;

        object IEnumerator.Current => Current;

        public virtual bool MoveNext()
        {
            if (CurrentIndex >= Points.Length)
                return false;

            CurrentIndex++;
            return true;
        }

        public void Reset()
        {
            CurrentIndex = 0;
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
