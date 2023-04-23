using UnityEngine;

namespace Kyzlyyk.CustomMesh
{
    public class MeshStructure_Readonly
    {
        public MeshStructure_Readonly(Vector3[] vertices, int[] triangles, Vector2[] uvs)
        {
            Vertices= vertices;
            Triangles= triangles;
            UVs= uvs;
        }

        public Vector3[] Vertices { get; }
        public int[] Triangles { get; }
        public Vector2[] UVs { get; }
    }
}