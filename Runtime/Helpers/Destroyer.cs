using UnityEngine;

namespace Kyzlyk.Helpers
{
    public sealed class Destroyer : MonoBehaviour
    {
        [ContextMenu(nameof(DestroySelf))]
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
