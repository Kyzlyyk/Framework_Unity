using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public class Circle : Elipse
    {
        public Circle(Vector2 center, float radius, int points, bool normalized = false) :
            base(new Vector2(radius, radius), center, points)
        {
            Radius = radius;

            Vector2[] circle = MathUtility.GetCircleMap(center, radius, points);
            for (int i = 0; i < circle.Length; i++)
            {
                if (normalized)
                    Points[i] = circle[i].normalized;
                else
                    Points[i] = circle[i];
            }
        }

        public float Radius { get; }
    }
}