using System.Collections.Generic;
using UnityEngine;

namespace Kyzlyyk.Utils
{
    public readonly struct MathUtils
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

        public static Vector2[] GetCircleMap(Vector2 center, float radius, int segments)
        {
            Vector2[] points = new Vector2[segments];

            for (int i = 0; i < segments; i++)
            {
                float angle = i / (float)segments * Mathf.PI * 2;

                points[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius + center;
            }

            return points;
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
    }
}