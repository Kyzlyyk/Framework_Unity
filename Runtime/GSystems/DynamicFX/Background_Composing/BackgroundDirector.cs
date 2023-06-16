using UnityEngine;

namespace Kyzlyk.LSGSystem.DynamicFX.BackgroundComposing
{
    [RequireComponent(typeof(BackgroundSwitcher))]
    public abstract class BackgroundDirector : MonoBehaviour
    {
        protected BackgroundSwitcher BackgroundSwitcher;

        private void Awake()
        {
            BackgroundSwitcher = GetComponent<BackgroundSwitcher>();
        }
    }
}