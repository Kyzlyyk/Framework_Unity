using UnityEngine;

namespace Kyzlyk.Helpers
{
    public sealed class Destroyer : MonoBehaviour
    {
        [SerializeField] private float _time;

        [ContextMenu(nameof(DestroySelf))]
        public void DestroySelf()
        {
            Destroy(gameObject, _time);
        }
    }
}
