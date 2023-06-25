using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kyzlyk.Helpers.Math
{
    public class Elipse : Shape, IEnumerator<Vector2>
    {
        public Elipse(Vector2 center, Vector2 size, int points, bool normalized = false)
        {
            _currentIndex = 0;
            Size = size;
            Center = center;

            Points = new Vector2[points];
            
            Vector2[] elipse = MathUtility.GetElipseMap(center, size, points);
            for (int i = 0; i < elipse.Length; i++)
            {
                if (normalized)
                    Points[i] = elipse[i].normalized;
                else
                    Points[i] = elipse[i];
            }
        }
        
        protected Elipse(Vector2 center, Vector2 size, int points)
        {
            _currentIndex = 0;
            Size = size;
            Center = center;

            Points = new Vector2[points];
        }

        public override Vector2 Size { get; }
        public override Quaternion Rotation { get; }
        public override Vector2 Center { get; }

        public Vector2[] Points { get; }
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

        public override float GetPerimeter()
        {
            throw new System.NotImplementedException();
        }

        public override float GetArea()
        {
            throw new System.NotImplementedException();
        }
    }
}
