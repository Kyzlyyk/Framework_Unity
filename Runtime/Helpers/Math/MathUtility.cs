using System.Collections.Generic;
using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public readonly struct MathUtility
    {
        public static Vector2[] GetSinusoidMap(Vector2 origin, float amplitude, float frequency, float phase, float length, int resolution)
        {
            Vector2[] points = new Vector2[resolution];

            float incrementX = length / (resolution - 1);
            float x = 0;

            for (int i = 0; i < resolution; i++)
            {
                float y = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * x + phase);

                points[i] = new Vector2(x, y) + origin;
                x += incrementX;
            }

            return points;
        }

        public static Vector2[] GetTangensoidMap(Vector2 origin, float xMax, float xMin, float amplitude, float frequency, float phase, float yOffset, int resolution)
        {
            List<Vector2> points = new List<Vector2>(resolution); 

            float xStep = (xMax - xMin) / (float)resolution;

            for (float x = xMin; x <= xMax; x += xStep)
            {
                float y = amplitude * Mathf.Tan(frequency * (x + phase)) + yOffset;
                points.Add(new Vector2(x, y) + origin);
            }

            return points.ToArray();
        }

        public static Vector2[] GetCircleMap(Vector2 p1, Vector2 p2, int segments)
        {
            Vector2[] circlePoints = new Vector2[segments];

            Vector2 center = (p1 + p2) / 2f;
            float radius = Vector2.Distance(center, p1);
            
            float angleIncrement = 2 * Mathf.PI / segments;
            float startAngle = Mathf.Atan2(p1.y - center.y, p1.x - center.x);

            for (int i = 0; i < segments; i++)
            {
                float angle = startAngle + i * angleIncrement;
                float x = center.x + radius * Mathf.Cos(angle);
                float y = center.y + radius * Mathf.Sin(angle);
                
                circlePoints[i] = new Vector2(x, y);
            }

            return circlePoints;
        }
        
        public static Vector2[] GetCircleMap(Vector2 center, float radius, int segments)
        {
            return GetElipseMap(center, new Vector2(radius, radius), segments);
        }
        
        public static Vector2[] GetElipseMap(Vector2 center, Vector2 size, int segments)
        {
            Vector2[] circlePoints = new Vector2[segments];

            float angleIncrement = 2 * Mathf.PI / segments;
            for (int i = 0; i < segments; i++)
            {
                float angle = i * angleIncrement;
                float x = Mathf.Cos(angle) * size.x;
                float y = Mathf.Sin(angle) * size.y;
                circlePoints[i] = new Vector2(x, y) + center;
            }

            return circlePoints;
        }

        public static Vector2[] GetLineMap(Vector2 startPoint, Vector2 endPoint, int resolution)
        {
            Vector2[] points = new Vector2[resolution];

            for (int i = 0; i < resolution; i++)
            {
                points[i] = Vector2.Lerp(startPoint, endPoint, (float)i / (resolution - 1));
            }

            return points;
        }

        public static Vector2[] GetParabolaMap(Vector2 origin, float a, float b, float c, float minX, float maxX, int resolution)
        {
            List<Vector2> points = new List<Vector2>();
            
            float stepSize = (maxX - minX) / resolution;

            for (float x = minX; x <= maxX; x += stepSize)
            {
                float y = a * x * x + b * x + c;
                points.Add(new Vector2(x, y) + origin);
            }

            return points.ToArray();
        }

        public static Vector2 GetVector(Vector2 a, Vector2 b) => b - a;

        public static Vector3 GetVector(Vector3 a, Vector3 b) => b - a;

        public static float ToRadius(Vector2 size) => (size.x + size.y) / Mathf.PI;
    }
}