using UnityEngine;

namespace Kyzlyk.LifeSupportModules.PowerUps
{
    public interface IPowerUpInteractor
    {
        GameObject Source { get; }
        
        void InteractWithPowerUp(PowerUp powerUp);
        void DeinteractWithPowerUp(PowerUp powerUp);
    }
}