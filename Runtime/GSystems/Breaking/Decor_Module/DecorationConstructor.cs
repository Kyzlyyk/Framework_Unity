using UnityEngine;
using Kyzlyk.Helpers;
using Kyzlyk.Helpers.Core;
using Kyzlyk.Helpers.GMesh;
using System.Collections.Generic;
using Kyzlyk.Helpers.Extensions;

namespace Kyzlyk.LSGSystem.Breaking.Modules
{
    public static class DecorationConstructor
    {
        public readonly struct Element
        {
            public Element(Vector3 position, float scale, float alphaAngle)
            {
                AlphaAngle = alphaAngle;
                Position = position;
                Scale = scale;
            }

            public Vector3 Position { get; }
            public float AlphaAngle { get; }
            public float Scale { get; }
        }

        public static MeshStructure_Shape GenerateDecoration(Vector2 position, IReadOnlyList<float> scaleRange, Corner cornerToStick, Dimension dimension, NoiseMap map)
        {
            MeshStructure meshStructure = new();

            while (map.MoveNext())
            {
                float shapeScale = scaleRange.Random();

                Vector3 shapePosition = map.Current + (Vector3)position;

                shapePosition = cornerToStick switch
                {
                    Corner.TopLeft => Vector3.up + shapePosition - new Vector3(0f, shapeScale),
                    Corner.TopRight => Vector3.one + shapePosition - new Vector3(shapeScale, shapeScale),

                    Corner.BottomLeft => shapePosition,
                    Corner.BottomRight => Vector3.right + shapePosition - new Vector3(shapeScale, 0f),

                    _ => shapePosition
                };

                SetPath(
                    meshStructure,
                    new Element
                    (
                        shapePosition,
                        shapeScale,
                        alphaAngle: 0f
                    ),
                    dimension
                );
            }

            return new MeshStructure_Shape(
                new MeshShape(meshStructure.Vertices, meshStructure.Triangles),
                meshStructure.UVs);
        }

        public static MeshStructure GenerateDecoration(Element[] elements, Dimension dimension)
        {
            MeshStructure meshStructure = new();

            for (int i = 0; i < elements.Length; i++)
            {
                SetPath(meshStructure, elements[i], dimension);
            }

            return meshStructure;
        }

        private static void SetPath(MeshStructure meshStructure, Element pathSet, Dimension dimension)
        {
            if (dimension == Dimension.ThreeD)
                GMeshUtility.AddCube(meshStructure, pathSet.Position, pathSet.Scale);
            else if (dimension == Dimension.TwoD)
                GMeshUtility.AddSquare(meshStructure, pathSet.Position, pathSet.Scale);
        }
    }
}