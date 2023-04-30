using UnityEngine;
using UnityEngine.UIElements;

namespace Kyzlyyk.CustomMesh
{
    public struct MeshUtility
    {
        public static void AddCube(MeshStructure meshStructure, Vector3 position, float scale = 1)
        {
            meshStructure.Vertices.AddRange(GetLeftSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure);
            
            meshStructure.Vertices.AddRange(GetRightSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure);
            
            meshStructure.Vertices.AddRange(GetFrontSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure);
            
            meshStructure.Vertices.AddRange(GetBackSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure);

            meshStructure.Vertices.AddRange(GetTopSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure);

            meshStructure.Vertices.AddRange(GetBottomSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure);
        }
        
        public static MeshStructure GetCube(Vector3 position, int verticesCount, float scale = 1)
        {
            MeshStructure meshStructure = new();

            meshStructure.Vertices.AddRange(GetLeftSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure, verticesCount + meshStructure.Vertices.Count);
            
            meshStructure.Vertices.AddRange(GetRightSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure, verticesCount + meshStructure.Vertices.Count);
            
            meshStructure.Vertices.AddRange(GetFrontSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure, verticesCount + meshStructure.Vertices.Count);
            
            meshStructure.Vertices.AddRange(GetBackSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure, verticesCount + meshStructure.Vertices.Count);

            meshStructure.Vertices.AddRange(GetTopSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure, verticesCount + meshStructure.Vertices.Count);

            meshStructure.Vertices.AddRange(GetBottomSideSquare(position, scale));
            SetLastVerticesOfTrianglesOfSquare(ref meshStructure, verticesCount + meshStructure.Vertices.Count);

            return meshStructure;
        }

        public static Vector3[] GetLeftSideSquare(Vector3 position, float scale)
        {
            return new Vector3[4]
            {
                (new Vector3(0f, 0f, 0f) + (position / scale)) * scale,
                (new Vector3(0f, 0f, 1f) + (position / scale)) * scale,
                (new Vector3(0f, 1f, 0f) + (position / scale)) * scale,
                (new Vector3(0f, 1f, 1f) + (position / scale)) * scale,
            };
        }
        
        public static Vector3[] GetRightSideSquare(Vector3 position, float scale)
        {
            return new Vector3[4]
            {
                (new Vector3(1f, 0f, 0f) + (position / scale)) * scale,
                (new Vector3(1f, 1f, 0f) + (position / scale)) * scale,
                (new Vector3(1f, 0f, 1f) + (position / scale)) * scale,
                (new Vector3(1f, 1f, 1f) + (position / scale)) * scale,
            };
        }
        
        public static Vector3[] GetFrontSideSquare(Vector3 position, float scale)
        {
            return new Vector3[]
            {
                (new Vector3(0f, 0f, 0f) + (position / scale)) * scale,
                (new Vector3(0f, 1f, 0f) + (position / scale)) * scale,
                (new Vector3(1f, 0f, 0f) + (position / scale)) * scale,
                (new Vector3(1f, 1f, 0f) + (position / scale)) * scale,
            };
        }
        
        public static Vector3[] GetBackSideSquare(Vector3 position, float scale)
        {
            return new Vector3[4]
            {
                (new Vector3(0f, 0f, 1f) + (position / scale)) * scale,
                (new Vector3(1f, 0f, 1f) + (position / scale)) * scale,
                (new Vector3(0f, 1f, 1f) + (position / scale)) * scale,
                (new Vector3(1f, 1f, 1f) + (position / scale)) * scale,
            };
        }
        
        public static Vector3[] GetBottomSideSquare(Vector3 position, float scale)
        {
            return new Vector3[4]
            {
                (new Vector3(0f, 0f, 0f) + (position / scale)) * scale,
                (new Vector3(1f, 0f, 0f) + (position / scale)) * scale,
                (new Vector3(0f, 0f, 1f) + (position / scale)) * scale,
                (new Vector3(1f, 0f, 1f) + (position / scale)) * scale,
            };
        }
        
        public static Vector3[] GetTopSideSquare(Vector3 position, float scale)
        {
            return new Vector3[4]
            {
                (new Vector3(0f, 1f, 0f) + (position / scale)) * scale,
                (new Vector3(0f, 1f, 1f) + (position / scale)) * scale,
                (new Vector3(1f, 1f, 0f) + (position / scale)) * scale,
                (new Vector3(1f, 1f, 1f) + (position / scale)) * scale,
            };
        }

        public static void AddSquare(MeshStructure meshStructure, Vector3 position, float scale = 1)
        {
            meshStructure.Vertices.Add((new Vector3(0, 0) + (position / scale)) * scale);
            meshStructure.Vertices.Add((new Vector3(0, 1) + (position / scale)) * scale);
            meshStructure.Vertices.Add((new Vector3(1, 0) + (position / scale)) * scale);
            meshStructure.Vertices.Add((new Vector3(1, 1) + (position / scale)) * scale);

            SetLastVerticesOfTrianglesOfSquare(ref meshStructure);

            meshStructure.UVs.Add(new Vector2(0f, 0f));
            meshStructure.UVs.Add(new Vector2(.5f, 1f));
            meshStructure.UVs.Add(new Vector2(1f, 0f));
            meshStructure.UVs.Add(new Vector2(.5f, -1f));
        }
        
        public static MeshStructure GetSquare(Vector3 position, int verticesCount, float scale = 1)
        {
            MeshStructure meshStructure = new();

            meshStructure.Vertices.Add((new Vector3(0, 0) + (position / scale)) * scale);
            meshStructure.Vertices.Add((new Vector3(0, 1) + (position / scale)) * scale);
            meshStructure.Vertices.Add((new Vector3(1, 0) + (position / scale)) * scale);
            meshStructure.Vertices.Add((new Vector3(1, 1) + (position / scale)) * scale);

            SetLastVerticesOfTrianglesOfSquare(ref meshStructure, verticesCount + meshStructure.Vertices.Count);

            meshStructure.UVs.Add(new Vector2(0f, 0f));
            meshStructure.UVs.Add(new Vector2(.5f, 1f));
            meshStructure.UVs.Add(new Vector2(1f, 0f));
            meshStructure.UVs.Add(new Vector2(.5f, -1f));

            return meshStructure;
        }

        private static void SetLastVerticesOfTrianglesOfSquare(ref MeshStructure meshStructure)
        {
            meshStructure.Triangles.Add(meshStructure.Vertices.Count - 4);
            meshStructure.Triangles.Add(meshStructure.Vertices.Count - 3);
            meshStructure.Triangles.Add(meshStructure.Vertices.Count - 2);

            meshStructure.Triangles.Add(meshStructure.Vertices.Count - 3);
            meshStructure.Triangles.Add(meshStructure.Vertices.Count - 1);
            meshStructure.Triangles.Add(meshStructure.Vertices.Count - 2);
        }
        
        private static void SetLastVerticesOfTrianglesOfSquare(ref MeshStructure meshStructure, int verticesCount)
        {
            meshStructure.Triangles.Add(verticesCount - 4);
            meshStructure.Triangles.Add(verticesCount - 3);
            meshStructure.Triangles.Add(verticesCount - 2);

            meshStructure.Triangles.Add(verticesCount - 3);
            meshStructure.Triangles.Add(verticesCount - 1);
            meshStructure.Triangles.Add(verticesCount - 2);
        }
    }
}