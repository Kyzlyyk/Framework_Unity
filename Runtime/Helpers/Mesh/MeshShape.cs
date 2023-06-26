using UnityEngine;
using System.Linq;
using Kyzlyk.Helpers.Math;
using System.Collections.Generic;

namespace Kyzlyk.Helpers.GMesh
{
    public class MeshShape
    {
        public MeshShape(IEnumerable<Vector3> vertices, IEnumerable<int> triangles)
        {
            if (triangles.Count() < vertices.Count())
                throw new System.ArgumentException("Property 'Length' of parameter 'triangles' must be equal or greater property 'Length' of parameter 'vertices'!");

            Vertices = vertices;
            _triangles = triangles;
        }

        public MeshShape(Vector3[] vertices, Shape2DType shape)
        {
            Vertices = vertices;

            int verticesCount = Vertices.Count();
            _triangles = shape switch
            {
                Shape2DType.Square => new int[]
                {
                    verticesCount - 4, verticesCount - 3, verticesCount - 2,
                    verticesCount - 3, verticesCount - 1, verticesCount - 2
                },

                Shape2DType.Triangle => new int[]
                {
                    verticesCount - 3, verticesCount - 2, verticesCount - 1
                },

                _ => new int[0]
            };
        }

        private readonly IEnumerable<int> _triangles;
        public IEnumerable<Vector3> Vertices { get; }

        public IEnumerable<int> GetTriangles(int shapeIndex)
        {
            return _triangles.Select(t => t + shapeIndex);
        }
    }
}