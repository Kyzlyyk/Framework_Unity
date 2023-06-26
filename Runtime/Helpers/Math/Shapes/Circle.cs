using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public class Circle : Elipse
    {
        public Circle(Vector2 center, float radius, int points) :
            base(new Vector2(radius, radius), center, points)
        {
            Radius = radius;
        }

        public float Radius { get; }

        public override float GetPerimeter()
        {
            return 2 * Mathf.PI * Radius;
        }

        public static Vector2[] GetCircleMap(Vector2 center, float radius, int segments)
        {
            return GetElipseMap(center, new Vector2(radius, radius), segments);
        }
    }
}