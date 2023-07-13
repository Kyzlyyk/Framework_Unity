using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public class Triangle : Shape2D
    {
        public Triangle(Vector2 a, Vector2 b, Vector2 c)
        {
            if (!IsTriangle(a, b, c))
            {
                Debug.LogError("Points: a, b, c are not a trinagle!");
                return;
            }

            A = a;
            B = b;
            C = c;
        }

        public Vector2 A { get; private set; }
        public Vector2 B { get; private set; }
        public Vector2 C { get; private set; }

        public override Vector2 Size => throw new System.NotImplementedException();
        public override Vector3 Rotation => throw new System.NotImplementedException();
        public override Vector2 Center => throw new System.NotImplementedException();
        public override Vector2[] Points => throw new System.NotImplementedException();

        private const float Inaccuracy = 0.01f;

        public override float GetArea()
        {
            return CalculateArea(A, B, C);
        }

        public override float GetPerimeter()
        {
            return GetSideLength(A, B) + GetSideLength(B, C) + GetSideLength(C, A); 
        }

        public static float CalculateArea(Vector2 a, Vector2 b, Vector2 c)
        {
            return Mathf.Abs(0.5f * (a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y)));
        }

        public bool Contains(Vector2 point)
        {
            return Contains(A, B, C, point);
        }
        
        public static bool Contains(Vector2 a, Vector2 b, Vector2 c, Vector2 point)
        {
            float abcArea = CalculateArea(a, b, c);
            float pabArea = CalculateArea(point, a, b);
            float pbcArea = CalculateArea(point, b, c);
            float pacArea = CalculateArea(point, a, c);

            float areasSum = pabArea + pbcArea + pacArea;
            
            return Mathf.Approximately(abcArea, areasSum);
        }

        private float GetSideLength(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        public bool IsTriangle()
        {
            return IsTriangle(A, B, C);
        }
        
        public static bool IsTriangle(Vector2 a, Vector2 b, Vector2 c)
        {
            if (a + b == c)
                if (b + c == a)
                    if (c + a == b)
                        return true;

            return false;
        }
    }
}
