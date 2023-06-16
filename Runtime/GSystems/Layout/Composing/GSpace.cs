using UnityEngine;
using System.Collections.Generic;
using Kyzlyk.Helpers.Extensions;
using Kyzlyk.Helpers;

namespace Kyzlyk.LSGSystem.Layout.Composing
{
    public abstract class GSpace : MonoBehaviour
    {
        public abstract Chunk CurrentChank { get; }

        [SerializeField] protected Transform[] UnfilledTransformPoints;

        public IAdder<Vector2> UnffiledPoints { get; set; } = new OnlyAddList<Vector2>();

        public abstract void Draw();
        public abstract IEnumerable<Chunk> GetAllChunks();

        protected void HandleUnffiledPoints(Chunk chunk)
        {
            if (UnfilledTransformPoints.IsNullOrEmpty()) return;

            Vector2[] unfilledTransformPoints = new Vector2[UnfilledTransformPoints.Length];
            for (int i = 0; i < UnfilledTransformPoints.Length; i++)
            {
                unfilledTransformPoints[i] = UnfilledTransformPoints[i].position;
            }

            chunk.Builder.RemoveGMaterials(unfilledTransformPoints);
            chunk.Builder.RemoveGMaterials(UnffiledPoints);
        }
    }
}