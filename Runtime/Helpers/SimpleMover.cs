using Kyzlyk.Helpers.Math;
using UnityEngine;

namespace Kyzlyk.Helpers
{
    /// <summary>
    /// The simplest mover, with just a Lerp update.
    /// </summary>
    public class SimpleMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Vector2 _direction;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public UnitVector Direction
        {
            get => _unitDirection;
            set => _unitDirection = value;
        }

        private UnitVector _unitDirection;

        private void OnValidate()
        {
            _unitDirection = (UnitVector)_direction;
        }

        private void Update()
        {
            Vector2 position2 = (Vector2)transform.position;

            transform.position =
                Vector2.Lerp(transform.position, 
                MathUtility.GetPoint(position2, Direction, new Vector2(Speed, Speed).magnitude),
                Time.deltaTime);
        }
    }
}
