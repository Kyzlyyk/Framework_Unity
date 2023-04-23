using System;
using UnityEngine;
using System.Collections.Generic;

namespace Kyzlyyk.Utils
{
    public class AbilityManager<TAbility> : MonoBehaviour where TAbility : Ability
    {
        private readonly KeyList<Type, TAbility> _activeAbilities = new(10);
        private readonly KeyList<Type, TAbility> _abilities = new(10);

        private readonly HashSet<IUpdateble> _updatebles = new();

        private void Update()
        {
            if (_updatebles.Count == 0) return;

            foreach (IUpdateble item in _updatebles)
            {
                item.Update();
            }
        }

        public void Add(TAbility ability)
        {
            ability.OnActivated += OnActivated;
            ability.OnDeactivated += OnDeactivated;
            ability.CoroutineStarted += OnCoroutineStarted;

            _abilities.Add(ability.GetType(), ability);
        }

        public void Remove<T>() where T : TAbility
        {
            if (!Get<T>(out TAbility ability))
                return;

            ability.OnActivated -= OnActivated;
            ability.OnDeactivated -= OnDeactivated;
            ability.CoroutineStarted -= OnCoroutineStarted;

            if (typeof(T).GetInterface(typeof(IUpdateble).Name) == typeof(IUpdateble))
                _updatebles.Remove((IUpdateble)ability);

            ability.ToRemoved();

            _activeAbilities.Remove(typeof(T));
            _abilities.Remove(typeof(T));
        }

        public void Activate<T>() where T : TAbility
        {
            if (
                _abilities.TryGetValue(typeof(T), out TAbility ability) &&
                !_activeAbilities.Contains(typeof(T))
                )
            {
                _activeAbilities.Add(typeof(T), ability);

                if (typeof(T).GetInterface(typeof(IUpdateble).Name) == typeof(IUpdateble))
                    _updatebles.Add((IUpdateble)ability);

                ability.ToActivated();
            }
        }
        
        public void Activate(Type abilityType)
        {
            if (
                _abilities.TryGetValue(abilityType, out TAbility ability) &&
                !_activeAbilities.Contains(abilityType)
                )
            {
                _activeAbilities.Add(abilityType, ability);

                if (abilityType.GetInterface(typeof(IUpdateble).Name) == typeof(IUpdateble))
                    _updatebles.Add((IUpdateble)ability);

                ability.ToActivated();
            }
        }

        public void Deactivate<T>() where T : TAbility
        {
            if (_activeAbilities.TryGetValue(typeof(T), out TAbility ability))
            {
                if (typeof(T).GetInterface(typeof(IUpdateble).Name) == typeof(IUpdateble))
                   _updatebles.Remove((IUpdateble)ability);

                ability.ToDeactivated();
                
                _activeAbilities.Remove(typeof(T));
            }
        }
        
        public void Deactivate(Type abilityType)
        {
            if (_activeAbilities.TryGetValue(abilityType, out TAbility ability))
            {
                if (abilityType.GetInterface(typeof(IUpdateble).Name) == typeof(IUpdateble))
                   _updatebles.Remove((IUpdateble)ability);

                ability.ToDeactivated();
                
                _activeAbilities.Remove(abilityType);
            }
        }

        public bool Get<T>(out TAbility ability) where T : TAbility
        {
            return _abilities.TryGetValue(typeof(T), out ability);
        }
        
        public bool Get(Type abilityType, out TAbility ability)
        {
            return _abilities.TryGetValue(abilityType, out ability);
        }

        private void OnActivated(object sender, Type abilityType)
        {
            Activate(abilityType);
        }

        private void OnDeactivated(object sender, Type abilityType)
        {
            Deactivate(abilityType);
        }

        private void OnCoroutineStarted(object sender, Type type)
        {
            Get(type, out TAbility ability);
            StartCoroutine(ability.ToCoroutine());
        }
    }
}