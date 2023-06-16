using UnityEngine;
using UnityEngine.EventSystems;

namespace Kyzlyk.Enviroment.UI
{
    public sealed class SwitcherButton : MonoBehaviour, IPointerClickHandler
    {
        public Switcher Switcher;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Switcher != null)
            {
                Switcher.Switch();
            }
        }
    }
}