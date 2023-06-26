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

        public static Vector2[] GetLineMap(Vector2 a, Vector2 b, int segments)
        {
            Vector2[] points = new Vector2[segments + 1];
            float segmentLength = 1f / segments;

            for (int i = 0; i <= segments; i++)
            {
                float t = i * segmentLength;
                points[i] = Vector2.Lerp(a, b, t);
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