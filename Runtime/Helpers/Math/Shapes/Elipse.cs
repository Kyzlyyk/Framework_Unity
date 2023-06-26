using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public class Elipse : Shape2D
    {
        public Elipse(Vector2 center, Vector2 size, int points)
        {
            Size = size;
            Center = center;
            
            Points = GetElipseMap(center, size, points);
        }

        public override Vector2 Size { get; }
        public override Quaternion Rotation { get; }
        public override Vector2 Center { get; }
        public override Vector2[] Points { get; }

        public override float GetPerimeter()
        {
            return Mathf.PI * (3 * (Size.x + Size.y) - Mathf.Sqrt((3 * Size.x + Size.y) * (Size.x + 3 * Size.y)));
        }

        public override float GetArea()
        {
            return Mathf.PI * (Size.x * Size.y);
        }

        public static Vector2[] GetElipseMap(Vector2 center, Vector2 size, int segments)
        {
            Vector2[] circlePoints = new Vector2[segments];

            float angleIncrement = 2 * Mathf.PI / segments;
            for (int i = 0; i < segments; i++)
            {
                float angle = i * angleIncrement;
                float x = (Mathf.Cos(angle) * size.x) + center.y;
                float y = (Mathf.Sin(angle) * size.y) + center.x;
                circlePoints[i] = new Vector2(x, y);
            }

            return circlePoints;
        }
    }
}
