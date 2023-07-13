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
        public override Vector3 Rotation
        {
            get
            {
                UnitVector radian = (UnitVector)GetElipseMap_Normalized(Size, Points.Length)[CurrentIndex];
                return new Vector3(0f, 0f, radian.X + radian.Y);
            }
        }
        public override Vector2 Center { get; }
        public override Vector2[] Points { get; }

        public override float GetPerimeter()
        {
            return Mathf.PI * (3f * (Size.x + Size.y) - Mathf.Sqrt((3f * Size.x + Size.y) * (Size.x + 3f * Size.y)));
        }

        public override float GetArea()
        {
            return Mathf.PI * (Size.x * Size.y);
        }

        public static Vector2[] GetElipseMap(Vector2 center, Vector2 size, int segments)
        {
            Vector2[] circlePoints = new Vector2[segments];

            float angleIncrement = 2f * Mathf.PI / segments;
            for (int i = 0; i < segments; i++)
            {
                float angle = i * angleIncrement;
                float x = (Mathf.Cos(angle) * size.x) + center.y;
                float y = (Mathf.Sin(angle) * size.y) + center.x;
                circlePoints[i] = new Vector2(x, y);
            }

            return circlePoints;
        }
        
        public static Vector2[] GetElipseMap_Normalized(Vector2 size, int segments)
        {
            return GetElipseMap(Vector2.zero, size.normalized, segments);
        }
    }
}
