using System.Collections.Generic;
using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public class Rectangle : Shape2D
    {
        public Rectangle(Vector2 center, Vector2 size, int points)
        {
            Center = center;
            Size = size;

            Points = GetRectangleMap(center, size, Mathf.CeilToInt(points / 4f));
        }

        public override Vector2 Size { get; }
        public override Vector3 Rotation { get; }
        public override Vector2 Center { get; }
        public override Vector2[] Points { get; }

        public override float GetArea()
        {
            return Size.x * Size.y;
        }

        public override float GetPerimeter()
        {
            return 2 * (Size.x + Size.y);
        }

        public static Vector2[] GetRectangleMap(Vector2 center, Vector2 size, int sideLength)
        {
            List<Vector2> points = new(sideLength * 4);
            
            Vector2 passage = new(center.x - (size.x / 2f), center.y + (size.y / 2f));

            // Left side
            float margin = size.y / sideLength;
            for (int i = 0; i < sideLength; i++)
            {
                points.Add(passage);
                passage.y -= margin;
            }

            // Bottom side
            margin = size.x / sideLength;
            for (int i = 0; i < sideLength; i++)
            {
                points.Add(passage);
                passage.x += margin;
            }

            // Right side
            margin = size.y / sideLength;
            for (int i = 0; i < sideLength; i++)
            {
                points.Add(passage);
                passage.y += margin;
            }

            // Top side
            margin = size.x / sideLength;
            for (int i = 0; i < sideLength; i++)
            {
                points.Add(passage);
                passage.x -= margin;
            }

            return points.ToArray();
        }
    }
}
