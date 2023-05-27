using System;
using UnityEngine;
using System.Linq;
using Kyzlyk.Helpers;
using Kyzlyk.Helpers.Extensions;
using System.Collections.Generic;
using Kyzlyk.LifeSupportModules.Breaking;

namespace Kyzlyk.Gameplay.PlayerComponents
{
    public enum HandleMode
    {
        TouchHandle = 1,
        IgnoreSurface = 2,
        IgnoreSurfaceAndTouchHandle = TouchHandle | IgnoreSurface,
        None = 0
    }

    public sealed class ShockWave : MonoBehaviour
    {
        [SerializeField] private Builder _builder;

        public delegate void TouchHandler(Builder builder, List<Vector2> touchPositions);
        public event TouchHandler OnTouched;

        private HandleMode _handleMode;

        public HandleMode HandleMode
        {
            get => _handleMode;

            set
            {
                if (value == HandleMode.IgnoreSurface || value == HandleMode.IgnoreSurfaceAndTouchHandle)
                    SetCollisionDetectionWithGMaterial(true);

                else if (_handleMode == HandleMode.IgnoreSurface || _handleMode == HandleMode.IgnoreSurfaceAndTouchHandle)
                    SetCollisionDetectionWithGMaterial(false);

                _handleMode = value;
            }
        }

        public Player Player { get; set; }

        public Transform BindTransform;

        [Space]
        public Vector2[] Shape = Array.Empty<Vector2>();

        private Vector3 _currentShockWavePosition;
        private SegmentBounds _bounds;

        private void Start()
        {
            SetPathBounds();

            if (BindTransform == null)
                BindTransform = transform;

            _currentShockWavePosition = BindTransform.position;
        }

        private void FixedUpdate()
        {
            if (HandleMode is HandleMode.TouchHandle or HandleMode.IgnoreSurfaceAndTouchHandle)
                Move();
        }

        private void Update()
        {
            if (HandleMode is HandleMode.TouchHandle or HandleMode.IgnoreSurfaceAndTouchHandle)
                ProcessTouches();
        }

        public void ProcessTouches()
        {
            List<GMaterialHitInfo> gMaterialHitInfos = new(Shape.Length);

            List<Vector2> touchPositions = new(Shape.Length);

            for (int i = 0; i < Shape.Length; i++)
            {
                bool isTouched;
                Vector2 touchPosition;

                (isTouched, touchPosition) = _builder.CheckWay(
                    _bounds.Segments[i].A.Floor(),
                    _bounds.Segments[i].B.Floor());

                if (isTouched)
                    touchPositions.Add(touchPosition);
            }

            if (OnTouched != null && touchPositions.Any())
                OnTouched(_builder, touchPositions);

            SetPathBounds();
        }

        public void SetPathBounds()
        {
            Segment[] segments = new Segment[Shape.Length];

            for (int i = 0; i < segments.Length; i++)
            {
                Vector2 b = i == segments.Length - 1
                    ? Shape[0]
                    : Shape[i + 1];

                segments[i] = new Segment(Shape[i], b);
            }

            _bounds = new SegmentBounds(segments);
        }

        public void SquarePath(Vector2 offset, float scale)
        {
            Shape = new[]
            {
                new Vector2(0, 0) * scale + offset,
                new Vector2(1, 0) * scale + offset,
                new Vector2(1, 1) * scale + offset,
                new Vector2(0, 1) * scale + offset
            };

            SetPathBounds();
        }

        private void Move()
        {
            for (int i = 0; i < Shape.Length; i++)
            {
                Shape[i] += (Vector2)(BindTransform.position - _currentShockWavePosition);
            }

            _currentShockWavePosition = BindTransform.position;
        }

        private void SetCollisionDetectionWithGMaterial(bool value)
        {
            Physics2D.IgnoreLayerCollision(
                        LayerMask.NameToLayer(LayerMask.LayerToName(BindTransform.gameObject.layer)),
                        LayerMask.NameToLayer("GMaterial"), value);
        }
    }
}