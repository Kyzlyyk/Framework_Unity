using System.Collections.Generic;
using UnityEngine;

namespace Kyzlyk.Helpers.GMesh
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public abstract class CustomMesh : MonoBehaviour
    {
        protected Mesh Mesh;

        protected readonly List<Vector3> Vertices = new();
        protected readonly List<int> Triangles = new();
        protected readonly List<Vector2> UVs = new();

        protected virtual void Awake()
        {
            Mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = Mesh;
        }

        /// <summary>
        /// Apply all changes and display them.
        /// </summary>
        public virtual void Apply()
        {
            Mesh.Clear();
            
            Mesh.SetVertices(Vertices);
            Mesh.SetTriangles(Triangles, 0);
            
            Mesh.SetUVs(0, UVs);
            
            Mesh.RecalculateNormals();
            Mesh.RecalculateBounds();
        }

        public virtual void Clear()
        {
            ClearStructure();
        }

        protected void ClearStructure()
        {
            Vertices.Clear();
            Triangles.Clear();
            UVs.Clear();
        }
    }
}