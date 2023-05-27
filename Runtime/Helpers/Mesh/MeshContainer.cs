using System.Collections.Generic;
using UnityEngine;

namespace Kyzlyk.Helpers.GMesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public abstract class MeshContainer<TKey> : CustomMesh
    {
        private readonly Dictionary<TKey, MeshStructure_Shape> _structure = new(50);

        public void Add(MeshStructure_Shape meshStructure, TKey key, bool apply = true)
        {
            if (!_structure.TryAdd(key, meshStructure)) 
            {
                Debug.LogAssertion("Upss, something went wrong! There is already the similar Mesh element.");
                
                return; 
            }
             
            Triangles.AddRange(meshStructure.MeshShape.GetTriangles(Vertices.Count));
            Vertices.AddRange(meshStructure.MeshShape.Vertices);
            UVs.AddRange(meshStructure.UVs);

            if (apply)
                Apply();
        }

        public void Remove(TKey key, bool apply = true)
        {
            if (!_structure.Remove(key)) return;

            ClearStructure();
            
            foreach (KeyValuePair<TKey, MeshStructure_Shape> pair in _structure)
            {
                Triangles.AddRange(pair.Value.MeshShape.GetTriangles(Vertices.Count));
                Vertices.AddRange(pair.Value.MeshShape.Vertices);
                UVs.AddRange(pair.Value.UVs);
            }

            if (apply)
                Apply();
        }

        public override void Reset()
        {
            base.Reset();
            
            _structure.Clear();
            
            Apply();
        }
    }
}