using UnityEngine;

namespace Kyzlyk.Gameplay.PlayerComponents
{
    public class Mover : MonoBehaviour, IJoystickHolder, IPhysicHandler
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _smooth;
        
        private Vector3 _velocity;

        public Rigidbody2D Rigidbody { get; set; }
        public Joystick Joystick { get; set; }
        public Player Player { get; set; }

        public void Update()
        {
            float horizontal = Joystick.Horizontal;
            float vertical = Joystick.Vertical;

            Vector3 targetVelocity = new Vector2(_speed * horizontal, _speed * vertical);

            Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, targetVelocity, ref _velocity, _smooth);
        }
    }
}