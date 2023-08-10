using System;
using UnityEngine;
using static UnityEngine.Mathf;
using static Kyzlyk.Helpers.Math.UnitVector;
using static Kyzlyk.Helpers.Math.MathUtility;
using Kyzlyk.Attributes.DebugAnnotations;

namespace Kyzlyk.Helpers.Math
{
    public readonly struct CircleSector
    {
        public CircleSector(float angle, float radius, Vector2 center, float degrees)
        {
            Angle = Abs(angle);
            Radius = radius;
            Degrees = degrees;
            Center = center;

            Arrowhead1 = GetPoint(Center, GetArrowheadVector(Degrees, Angle), Radius);
            Arrowhead2 = GetPoint(Center, GetArrowheadVector(Degrees, -Angle), Radius);
        }

        public CircleSector(float angle, UnitVector direction)
        {
            Angle = Abs(angle);
            Radius = 1f;
            Degrees = VectorToDeg360(direction);
            Center = Vector2.zero;

            Arrowhead1 = GetPoint(Center, GetArrowheadVector(Degrees, Angle), Radius);
            Arrowhead2 = GetPoint(Center, GetArrowheadVector(Degrees, -Angle), Radius);
        }

        public float Angle { get; }
        public float Radius { get; }
        public float Degrees { get; }
        public Vector2 Center { get; }
        public Vector2 Arrowhead1 { get; }
        public Vector2 Arrowhead2 { get; }

        public UnitVector Arrowhead1Vector => GetArrowheadVector(Degrees, Angle);
        public UnitVector Arrowhead2Vector => GetArrowheadVector(Degrees, -Angle);

        private static UnitVector GetArrowheadVector(float degrees, float angle) =>
            Deg360ToVector(degrees + angle * 0.5f);

#if false
        [NotCompleted]
        public bool Contains(Vector2 point)
        {
            if (Circle.Contains(point, Center, Radius))
            {
                static float Deg(UnitVector vector) => VectorToDeg360(vector);

                float arrowhead1Degree = Deg(Arrowhead1Vector);
                float arrowhead2Degree = Deg(Arrowhead2Vector);

                float pointDegree = Deg((UnitVector)point);

                bool Within(float start, float end, float value)
                    => start <= value && value <= end;

                Debug.Log("Min: " + Min(arrowhead1Degree, arrowhead2Degree));
                Debug.Log("Point: " + Deg((UnitVector)point));
                Debug.Log("Max: " + Max(arrowhead1Degree, arrowhead2Degree));

                float minDegree = Min(arrowhead1Degree, arrowhead2Degree);
                float maxDegree = Max(arrowhead1Degree, arrowhead2Degree);

                return true;
            }

            return false;
        }
#endif

        public Vector2 GetArcPoint(float degrees)
        {
            return GetArcPoint(Deg360ToVector(degrees));
        }

        public Vector2 GetArcPoint(UnitVector vector)
        {
            return GetPoint(Center, vector, Radius);
        }

        public void GoBetweenArrows(float degreesStep, Action<UnitVector> vectorHandler)
        {
            float startDegree = VectorToDeg180(Arrowhead1Vector);
            GoBetweenDegrees(startDegree, startDegree + Angle, degreesStep, vectorHandler);
        }

        public static void GoBetweenDegrees(
            float startDegree,
            float endDegree,
            float degreesStep,
            Action<UnitVector> vectorHandler
        )
        {
            if (degreesStep == 0)
                return;

            int times = RoundToInt((endDegree - startDegree) / degreesStep);
            float currentDegrees = startDegree;
            for (int i = 0; i < times; i++)
            {
                vectorHandler(Deg360ToVector(currentDegrees));
                currentDegrees -= degreesStep;
            }
        }

        public static void GoBetweenDegrees_Safe(
            float degrees1,
            float degrees2,
            float degreesStep,
            Action<UnitVector> vectorHandler
        )
        {
            GoBetweenDegrees(
                startDegree: Min(degrees1, degrees2),
                endDegree: Max(degrees1, degrees2),
                degreesStep: Abs(degreesStep),
                vectorHandler: vectorHandler
            );
        }
    }
}
