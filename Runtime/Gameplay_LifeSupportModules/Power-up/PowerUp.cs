using UnityEngine;

namespace Kyzlyk.LifeSupportModules.PowerUps
{
    public abstract class PowerUp : MonoBehaviour
    {
        public virtual IPowerUpInteractor Target { get; protected set; }

        protected abstract void Pickup();
        protected abstract void ResetEffect();
        
        public void Remove()
        {
            Target.DeinteractWithPowerUp(this);
            ResetEffect();
            
            Target = null;
        }

        public void Pickup(IPowerUpInteractor interactor)
        {
            Target = interactor;
            if (Target == null) return;

            Pickup();
            Target.InteractWithPowerUp(this);
        }
    }
    
    public abstract class PowerUp<TPowerUpInteractor> : PowerUp where TPowerUpInteractor : IPowerUpInteractor
    {
        private IPowerUpInteractor _target;

        public override IPowerUpInteractor Target
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