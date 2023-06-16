using UnityEngine;
using System.Linq;
using Kyzlyk.Helpers.GMesh;
using Kyzlyk.Helpers.Extensions;
using System.Collections.Generic;
using Kyzlyk.LSGSystem.Breaking.Modules;

namespace Kyzlyk.LSGSystem.Breaking
{
    public sealed class Builder : CustomMesh
    {
        public const byte GMaterialSidesCount = 4;

        [SerializeField] private Dimension _dimension;
        public Dimension Dimension => _dimension;

        public Supervisor Supervisor { get; private set; }

        private readonly Dictionary<Vector2Int, MeshStructure> _structure = new(50);

        private new void Awake()
        {
            base.Awake();

            Supervisor = new(this);
        }

        public bool HasGMaterial(Vector2 position)
        {
            return _structure.ContainsKey(position.ToVector2Int());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="applyChanges">If true, all marked GMaterials will be removed immediately.
        /// Otherwise, call the Apply() method to apply the changes.</param>
        public void RemoveGMaterial(Vector2 position, bool applyChanges = true)
        {
            RemoveGMaterial(position);

            UpdateMeshStructure();

            if (applyChanges)
                Apply();
        }

        /// <summary>
        /// Use it if you remove a lot of GMaterials to optimize.
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="applyChanges">If true, all marked GMaterials will be removed immediately.
        /// Otherwise, call the Apply() method to apply the changes. </param>
        public void RemoveGMaterials(IEnumerable<Vector2> positions, bool applyChanges = true)
        {
            if (!positions.Any()) return;

            foreach (Vector2 position in positions)
                RemoveGMaterial(position);

            UpdateMeshStructure();

            if (applyChanges)
                Apply();
        }

        private void RemoveGMaterial(Vector2 position)
        {
            Vector2Int positionInt = position.ToVector2Int();

            if (_structure.Remove(positionInt)) 
                Supervisor.UnwrapGMaterial(positionInt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>    
        /// <param name="texture"></param>
        /// <param name="applyChanges">If true, all marked GMaterials will be displayed immediately.
        /// Otherwise, call the Apply() method to apply and display the changes.</param>
        public void SpawnGMaterial(Vector2 position, GMaterialTexture texture, bool applyChanges = true)
        {
            Vector2Int positionInt = position.ToVector2Int();

            if (_structure.ContainsKey(positionInt)) return;

            MeshStructure meshStructure = _dimension switch
            {
                Dimension.TwoD => MeshUtility.GetSquare((Vector2)positionInt, Vertices.Count),
                Dimension.ThreeD => MeshUtility.GetCube((Vector2)positionInt, Vertices.Count),
                
                _ => null,
            };

            _structure.Add(positionInt, meshStructure);

            Vertices.AddRange(meshStructure.Vertices);
            Triangles.AddRange(meshStructure.Triangles);

            // working with texture parameter...
            UVs.AddRange(meshStructure.UVs);

            Supervisor.WrapGMaterial(position.ToVector2Int());

            if (applyChanges)
                Apply();
        }

        public override void Reset()
        {
            base.Reset();

            _structure.Clear();

            Supervisor.UnwrapAll();

            Apply();
        }

        public override void Apply()
        {
            base.Apply();

            Supervisor.ApplyWrap();
        }

        private void UpdateMeshStructure()
        {
            ClearStructure();

            void AddTriangles(int offset)
            {
                Triangles.Add(offset - 4);
                Triangles.Add(offset - 3);
                Triangles.Add(offset - 2);

                Triangles.Add(offset - 3);
                Triangles.Add(offset - 1);
                Triangles.Add(offset - 2);
            }

            int length = 0;

            if (_dimension == Dimension.TwoD)
                length = 1;
            
            else if (_dimension == Dimension.ThreeD)
                length = 6;

            foreach (var gmaterial in _structure)
            {
                Vertices.AddRange(gmaterial.Value.Vertices);

                for (int i = 0, j = 0; i < length; i++, j += 4)
                {
                    AddTriangles(Vertices.Count - j);
                }
                
                UVs.AddRange(gmaterial.Value.UVs);
            }
        }
    }
}