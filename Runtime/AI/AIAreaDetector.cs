using System;
using UnityEngine;
using Kyzlyk.Helpers;
using Kyzlyk.Helpers.Utils;

namespace Kyzlyk.AI
{
    public class AIAreaDetector : MonoBehaviour
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private Vector2 _offset;

        [Space]
        [SerializeField] private string[] _detectionLayers;

        [Space]
        public CircleOrRectangle DetectionAreaShape;

        public Vector2 Center => _offset + (Vector2)transform.position;
        public Vector2 Size => _size;
        public float Radius => MathUtility.ToRadius(Size);

        public bool AnyEntityDetected => AllDetectedEntities.Length > 0;
        
        public delegate void EntityDetectionHandler(Collider2D[] before, Collider2D[] after);
        public event EntityDetectionHandler EntityExited;
        public event EntityDetectionHandler EntityEntered;
        
        public Collider2D[] AllDetectedEntities { get; private set; } = new Collider2D[0];

        private int _detectionLayerMask;

        private void Start()
        {
            _detectionLayerMask = LayerMask.GetMask(_detectionLayers);
        }

        private void FixedUpdate()
        {
            OverlapEntities(); 
        }

        private void OverlapEntities()
        {
            if (DetectionAreaShape == CircleOrRectangle.Rectangle)
                AddDetectedEntity(Physics2D.OverlapBoxAll(Center, Size, 0f, _detectionLayerMask));

            else if (DetectionAreaShape == CircleOrRectangle.Circle)
                AddDetectedEntity(Physics2D.OverlapCircleAll(Center, Radius, _detectionLayerMask));
        }

        private void AddDetectedEntity(Collider2D[] colliders)
        {
            int lastCount = AllDetectedEntities.Length;

            if (lastCount > colliders.Length)
            {
                EntityExited?.Invoke(AllDetectedEntities, colliders);
                AllDetectedEntities = colliders;

                return;
            }

            if (colliders.Length == 0)
            {
                if (AllDetectedEntities.Length > 0)
                    AllDetectedEntities = Array.Empty<Collider2D>();

                return;
            }

            if (lastCount < colliders.Length)
                EntityEntered?.Invoke(AllDetectedEntities, colliders);

            AllDetectedEntities = colliders;
        }
    }
}