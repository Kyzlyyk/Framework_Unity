using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public class Elipse : Shape2D
    {
        public Elipse(Vector2 center, Vector2 size, int points, bool normalized = false)
        {
            Size = size;
            Center = center;
            
            Vector2[] elipse = MathUtility.GetElipseMap(center, size, points);
            for (int i = 0; i < elipse.Length; i++)
            {
                if (normalized)
                    Points[i] = elipse[i].normalized;
                else
                    Points[i] = elipse[i];
            }
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
            return Mathf.PI * Size.x * Size.y;
        }
    }
}
