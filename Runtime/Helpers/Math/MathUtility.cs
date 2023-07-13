using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.Mathf;

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
                float y = amplitude * Sin(2 * PI * frequency * x + phase);

                points[i] = new Vector2(x, y) + origin;
                x += incrementX;
            }

            return points;
        }

        public static Vector2[] GetTangensoidMap(Vector2 origin, float xMax, float xMin, float amplitude, float frequency, float phase, float yOffset, int resolution)
        {
            List<Vector2> points = new(resolution);

            float xStep = (xMax - xMin) / (float)resolution;

            for (float x = xMin; x <= xMax; x += xStep)
            {
                float y = amplitude * Tan(frequency * (x + phase)) + yOffset;
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
            List<Vector2> points = new();

            float stepSize = (maxX - minX) / resolution;

            for (float x = minX; x <= maxX; x += stepSize)
            {
                float y = a * x * x + b * x + c;
                points.Add(new Vector2(x, y) + origin);
            }

            return points.ToArray();
        }

        public static Vector2 GetVector(Vector2 origin, UnitVector direction, float length)
        {
            direction.Normalize_Internal();
            return new Vector2(origin.x + direction.X * length, origin.y + direction.Y * length);
        }

        public static float Angle180To360(float angle)
        {
            return angle < 0 ? 360f - Abs(angle) : angle;
        }
        
        public static float Angle360To180(float angle)
        {
            if (angle > 180f && angle <= 360f)
                return angle - 360f;

            return angle;
        }

        public static Vector2 RotateVector(Vector2 vectorToRotate, float angle)
        {
            float radians = DegToRad(angle);
            float cos = Cos(radians);
            float sin = Sin(radians);

            return new Vector2(
                x: vectorToRotate.x * cos - vectorToRotate.y * sin,
                y: vectorToRotate.x * sin + vectorToRotate.y * cos);
        }

        public static UnitVector RotateVector(UnitVector vectorToRotate, float angle)
        {
            return (UnitVector)RotateVector((Vector2)vectorToRotate, angle);
        }

        public static float DegToRad(float degrees) => degrees * Deg2Rad;
        public static float RadToDeg(float rad) => rad * Rad2Deg;
        public static Vector2 GetVector(Vector2 a, Vector2 b) => b - a;
        public static Vector3 GetVector(Vector3 a, Vector3 b) => b - a;
        public static float ToRadius(Vector2 size) => (size.x + size.y) / PI;
    }
}