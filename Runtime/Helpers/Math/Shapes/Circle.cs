using System;
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

        public static bool Contains(Vector2 point, Vector2 center, float radius)
        {
            Vector2 d = point - center;
            return d.sqrMagnitude <= radius * radius;
        }
    }
}