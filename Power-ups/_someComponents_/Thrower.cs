using System;
using UnityEngine;
using Gameplay.PowerUps;
using Helpers.Extensions;
using System.Collections.Generic;

namespace Gameplay.SomeComponents
{
    public sealed class Thrower : MonoBehaviour, /* IPhysicHandler, IJoystickHolder, ITurnSwitchable, */ IPowerUpInteractor
    {
        public ElasticitySettings ElasticitySettings;

        /*
        [Space]
        [Tooltip("When this object's velocity will be less than this value, then turn is end and control give to next " + nameof(ITurnSwitchable) + " object")]
        [SerializeField] private float _minVelocity;
        */
        
        public event EventHandler OnTurnEnd;

        GameObject IPowerUpInteractor.Source => gameObject;
        private readonly HashSet<IPowerUpController> _activePowerUps = new(2);

        public Rigidbody2D Rigidbody { get; set; }
        public Joystick Joystick { get; set; }
        public Player Player { get; set; }

        private Vector2 _previousVelocity;
        private Vector2 _throwVector;
        
        private bool _isJoystickHold;
        private bool _throwed;
        private bool _controlBlocked;

        private void FixedUpdate()
        {
            Rigidbody.velocity += (-_previousVelocity) / ElasticitySettings.DecelerationTime;

            if (VelocityIsApproximatelyZero())
            {
                if (_throwed)
                {
                    OnTurnEnd(this, EventArgs.Empty);
                    _throwed = false;
                }
            }
        }

        private void Update()
        {
            _previousVelocity = Rigidbody.velocity;

            if (_controlBlocked) return;

            if (CheckJoystickReleased())
            {
                Throw();
                BlockControl();

                _isJoystickHold = false;

                return;
            }

            if (CheckJoystickHold())
            {
                _throwVector = GetThrowVector();
                _isJoystickHold = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            float speed = _previousVelocity.magnitude;
            float forcef = Mathf.Min(ElasticitySettings.ReflectionForce * speed, ElasticitySettings.ReflectionForce);

            Vector3 direction = Vector3.Reflect(_previousVelocity.normalized, collision.GetContact(0).normal);
            Vector2 force = forcef * direction;

            Rigidbody.AddForce(force, ElasticitySettings.ForceMode);
        }

        private void Throw()
        {
            Rigidbody.AddForce(_throwVector * ElasticitySettings.ThrowForce, ElasticitySettings.ForceMode);
            _throwed = true;
        }

        private bool CheckJoystickHold()
        {
            return Joystick.Vertical != 0 || Joystick.Horizontal != 0;
        }

        private bool CheckJoystickReleased()
        {
            return _isJoystickHold && (Joystick.Vertical == 0 && Joystick.Horizontal == 0);
        }

        private void BlockControl()
        {
            _controlBlocked = true;
        }

        private void UnblockControl()
        {
            _controlBlocked = false;
        }

        private bool VelocityIsApproximatelyZero()
        {
            //return (Mathf.Abs(Rigidbody.velocity.x) < _minVelocity) && Mathf.Abs(Rigidbody.velocity.y) < _minVelocity;
            return false;
        }
       
        public Vector3 GetThrowVector()
        {
            return new Vector3(-Joystick.Horizontal, -Joystick.Vertical);
        }

        void ITurnSwitchable.EndTurn()
        {
            BlockControl();
            _activePowerUps.ForEach(p => p.Pause());
        }

        void ITurnSwitchable.StartTurn()
        {
            UnblockControl();
            _activePowerUps.ForEach(p => p.Resume());
        }

        void IPowerUpInteractor.InteractWithPowerUp(PowerUp powerUp)
        {
            if (powerUp is IPowerUpController c)
                _activePowerUps.Add(c);
        }

        void IPowerUpInteractor.DeinteractWithPowerUp(PowerUp powerUp)
        {
            if (powerUp is IPowerUpController c)
                _activePowerUps.Remove(c);
        }
    }
}