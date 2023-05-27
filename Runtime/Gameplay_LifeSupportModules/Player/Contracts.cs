using System;
using UnityEngine;

namespace Kyzlyk.LifeSupportModules.Player
{
    public interface IPlayerComponent
    {
        Player Player { get; set; }
    }

    public interface ITurnSwitchable : IPlayerComponent
    {
        event EventHandler OnTurnEnd;

        void EndTurn();
        void StartTurn();
    }

    public interface IJoystickHolder : IPlayerComponent
    {
        Joystick Joystick { get; set; }
    }

    public interface IPhysicHandler : IPlayerComponent
    {
        Rigidbody2D Rigidbody { get; set; }
    }
}