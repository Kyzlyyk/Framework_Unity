using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public class Circle : Elipse
    {
        public Circle(Vector2 center, float radius, int points, bool normalized = false) :
            base(new Vector2(radius, radius), center, points, normalized)
        {
            Radius = radius;
        }

        public float Radius { get; }

        public override float GetPerimeter()
        {
            return 2 * Mathf.PI * Radius;
        }
    }
}