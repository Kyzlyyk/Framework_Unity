using UnityEngine;

namespace Kyzlyk.Gameplay.PowerUps
{
    [RequireComponent(typeof(Collider2D))]
    public class PowerUpShell_Trigger : MonoBehaviour
    {
        [SerializeField] private PowerUp _powerUp;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Collider2D _collider;

        private void Awake()
        {
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent<IPowerUpInteractor>(out var target)) return;
            
            _powerUp.Pickup(target);

            if (_renderer != null)
                _renderer.enabled = false;
            
            _collider.enabled = false;
        }
    }
}