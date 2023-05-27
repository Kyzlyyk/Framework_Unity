using UnityEngine;
using Gameplay.PlayerComponents;

namespace Kyzlyk.Gameplay.PowerUps
{
    public class PowerUp_Elasticity : PowerUp<Thrower>
    {
        [SerializeField] private ElasticitySettings _settings;

        private ElasticitySettings _previousSettings;

        protected override void Pickup()
        {
            _previousSettings = DirectTarget.ElasticitySettings;
            DirectTarget.ElasticitySettings = _settings;
        }

        protected override void ResetEffect()
        {
            DirectTarget.ElasticitySettings = _previousSettings;
        }
    }
}