using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Kyzlyk.LifeSupportModules.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Player : MonoBehaviour, IComparable<Player>
    {
        [SerializeField] private char _group;
        [SerializeField] private Joystick _joystick;

        public int ID { get; private set; }
        public char Group => _group;

        public ITurnSwitchable TurnSwitchable { get; private set; }
        public IPhysicHandler PhysicalHandler { get; private set; }
        public IJoystickHolder JoystickHolder { get; private set; }

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            ID = GetHashCode();

            MonoBehaviour[] allComponents = GetComponents<MonoBehaviour>();

            IEnumerable<IPlayerComponent> playerComponents = allComponents.OfType<IPlayerComponent>();
            IEnumerable<IJoystickHolder> joystickHolders = allComponents.OfType<IJoystickHolder>();
            IEnumerable<IPhysicHandler> physicals = allComponents.OfType<IPhysicHandler>();
            IEnumerable<ITurnSwitchable> switchables = allComponents.OfType<ITurnSwitchable>();

            static string GetException(string nameOfComponent) => 
                $"Player object should have only one {nameOfComponent} component.";

            foreach (var item in playerComponents)
            {
                item.Player = this;
            }

            if (switchables.Count() > 1)
            {
                throw new System.Exception(GetException(nameof(ITurnSwitchable)));
            }
            else if (switchables.Count() > 0)
            {
                TurnSwitchable = switchables.First();
            }
            
            if (physicals.Count() > 1)
            {
                throw new System.Exception(GetException(nameof(IPhysicHandler)));
            }
            else if (physicals.Count() > 0)
            {
                PhysicalHandler = physicals.First();
                PhysicalHandler.Rigidbody = _rigidbody;
            }
            
            if (joystickHolders.Count() > 1)
            {
                throw new System.Exception(GetException(nameof(IJoystickHolder)));
            }
            else if (joystickHolders.Count() > 0)
            {
                JoystickHolder = joystickHolders.First();
                JoystickHolder.Joystick = _joystick;
            }
        }

        public int CompareTo(Player other)
        {
            return (other.ID > this.ID) ? other.ID : this.ID;
        }
    }
}