using System;
using System.Collections;
using UnityEngine;

namespace Kyzlyyk.Utils
{
    public interface IUpdateble
    {
        void Update();
    }

    public abstract class Ability
    {
        public Ability(GameObject parent)
        {
            Parent = parent;
        }

        public delegate void AbilityHandler(object sender, Type AbilityType);

        public event AbilityHandler OnActivated;
        public event AbilityHandler OnDeactivated;

        public virtual event AbilityHandler CoroutineStarted;

        public GameObject Parent { get; }

        public bool Active { get; private set; }

        /// <summary>
        /// Use it when activating Ability.
        /// </summary>
        public virtual void ToActivated() { }
        
        /// <summary>
        /// Use it when deactivating Ability.
        /// </summary>
        public virtual void ToDeactivated() { }

        /// <summary>
        /// Use it when Ability is removed.
        /// </summary>
        public virtual void ToRemoved() { }

        /// <summary>
        /// Use it when event 'CoroutineStarted' is invoked. (MonoBehaviour.StartCoroutine(IEnumerator routine)).
        /// </summary>
        /// <returns>Coroutine.</returns>
        public virtual IEnumerator ToCoroutine()
        {
            CoroutineStarted?.Invoke(this, GetType());

            yield break;
        }

        public void Activate()
        {
            Active = true;

            OnActivated?.Invoke(this, GetType());
        }

        public void Deactivate()
        {
            Active = false;
            
            OnDeactivated?.Invoke(this, GetType());
        }
    }
}