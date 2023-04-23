using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kyzlyyk.CustomMesh
{
    public class MeshStructure : IEquatable<MeshStructure>
    {
        public MeshStructure(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
        {
            Triangles = triangles;
            Vertices = vertices;
            UVs = uvs;
        }
        
        public MeshStructure()
        {
            Vertices = new List<Vector3>();
            Triangles = new List<int>();
            UVs = new List<Vector2>();
        }

        public List<Vector3> Vertices { get; }
        public List<int> Triangles { get; }
        public List<Vector2> UVs { get; }

        public override bool Equals(object obj)
        {
            return obj is MeshStructure structure && Equals(structure);
        }

        public bool Equals(MeshStructure other)
        {
            return EqualityComparer<List<Vector3>>.Default.Equals(Vertices, other.Vertices);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vertices);
        }

        public static bool operator ==(MeshStructure left, MeshStructure right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MeshStructure left, MeshStructure right)
        {
            return !(left == right);
        }
    }
}