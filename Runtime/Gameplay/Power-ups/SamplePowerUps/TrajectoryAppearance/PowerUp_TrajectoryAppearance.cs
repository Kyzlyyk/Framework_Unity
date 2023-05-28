using UnityEngine;
using Kyzlyk.Helpers;
using Kyzlyk.Helpers.Extensions;
using Gameplay.PlayerComponents;

namespace Kyzlyk.Gameplay.PowerUps
{
    public class PowerUp_TrajectoryAppearance : PowerUp<Thrower>, IPowerUpController
    {
        [SerializeField] private int _parts;

        private LineRenderer _lineRenderer;
        private Vector2 _previousThrowVector;

        private bool _paused = true;

        private void Update()
        {
            if (_paused) return;

            Vector2 throwVector = DirectTarget.GetThrowVector();

            if (throwVector == Vector2.zero)
                RenderTrajectory(0);
            
            else if (!_previousThrowVector.Compare(throwVector))
                RenderTrajectory(_parts);

            _previousThrowVector = throwVector;
        }

        private void RenderTrajectory(int parts)
        {
            if (parts == 0)
            {
                _lineRenderer.positionCount = 0;
                return;
            }
            
            Vector3[] path = new Vector3[parts];

            Vector2 nextDirection = DirectTarget.GetThrowVector();
            path[0] = Target.Source.transform.position;

            for (int i = 1; i < parts; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(path[i - 1], nextDirection, 20, LayerMask.GetMask(GLOBAL_CONSTANTS.GMaterialLayer));

                //print(hit.normal);
                
                path[i] = hit.point;
                nextDirection = Vector2.Reflect(nextDirection, hit.normal);
            }

            _lineRenderer.positionCount = parts;
            _lineRenderer.SetPositions(path);
        }

        protected override void Pickup()
        {
            if (DirectTarget.TryGetComponent<LineRenderer>(out var lineRenderer))
            {
                _lineRenderer = lineRenderer;
                _lineRenderer.enabled = true;
            }
            else
            {
                _lineRenderer = Target.Source.AddComponent<LineRenderer>();
            }

            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;

            _paused = false;
        }

        protected override void ResetEffect()
        {
            Destroy(gameObject);
        }

        public void Pause()
        {
            _paused = true;
            _lineRenderer.enabled = false;
        }

        public void Resume()
        {
            _paused = false;
            _lineRenderer.enabled = true;
        }
    }
}