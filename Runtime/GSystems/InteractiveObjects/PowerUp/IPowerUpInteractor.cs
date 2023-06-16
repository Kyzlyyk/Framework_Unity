using UnityEngine;

namespace Kyzlyk.LSGSystem.InteractiveObjects.PowerUps
{
    public interface IPowerUpInteractor<TInteractable> where TInteractable : PowerUp
    {
        GameObject Source { get; }
        
        void InteractWith(TInteractable interactable);
        void DeinteractWith(TInteractable interactable);
    }
}