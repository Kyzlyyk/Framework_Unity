using UnityEngine;

namespace Kyzlyk.LSGSystem.InteractiveObjects.PowerUps
{
    public abstract class PowerUp : MonoBehaviour
    {
        public virtual IPowerUpInteractor<PowerUp> Target { get; protected set; }

        protected abstract void Pickup();
        protected abstract void ResetEffect();
        
        public void Remove()
        {
            Target.DeinteractWith(this);
            ResetEffect();
            
            Target = null;
        }

        public void Pickup(IPowerUpInteractor<PowerUp> interactor)
        {
            Target = interactor;
            if (Target == null) return;

            Pickup();
            Target.InteractWith(this);
        }
    }
    
    public abstract class PowerUp<TPowerUpInteractor> : PowerUp
    {
        private IPowerUpInteractor<PowerUp> _target;

        public override IPowerUpInteractor<PowerUp> Target
        {
            get => _target;
            protected set
            {
                if (value is TPowerUpInteractor)
                    _target = value;
            }
        }

        public TPowerUpInteractor DirectTarget => (TPowerUpInteractor)Target;
    }
}