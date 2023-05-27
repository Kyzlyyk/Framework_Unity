using System.Collections.Generic;
using System;
using UnityEngine;

namespace Kyzlyk.Helpers.GMesh
{
    public class MeshStructure_Shape : IEquatable<MeshStructure_Shape>
    {
        public MeshStructure_Shape(MeshShape meshShape, IReadOnlyList<Vector2> uvs)
        {
            MeshShape = meshShape;
            UVs = uvs;
        }

        public MeshStructure_Shape(MeshShape meshShape)
        {
            MeshShape = meshShape;
            UVs = new List<Vector2>();
        }

        public MeshShape MeshShape { get; }
        public IReadOnlyList<Vector2> UVs { get; }

        public override bool Equals(object obj)
        {
            return obj is MeshStructure_Shape structure && Equals(structure);
        }

        public bool Equals(MeshStructure_Shape other)
        {
            return EqualityComparer<IEnumerable<Vector3>>.Default.Equals(MeshShape.Vertices, other.MeshShape.Vertices);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MeshShape.Vertices) + (int)DateTime.Now.ToBinary();
        }

        public static bool operator ==(MeshStructure_Shape left, MeshStructure_Shape right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MeshStructure_Shape left, MeshStructure_Shape right)
        {
            return !(left == right);
        }
    }
}