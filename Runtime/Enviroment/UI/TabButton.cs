using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kyzlyk.Enviroment.UI
{
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public TabGroup TabGroup;
        public Image Background;

        public UnityEvent OnSelected;
        public UnityEvent OnDeselected;

        public void OnPointerClick(PointerEventData eventData)
        {
            TabGroup.OnTabSelected(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TabGroup.OnTabHover(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TabGroup.OnTabHoverExit(this);
        }

        public void Select()
        {
            OnSelected?.Invoke();
        }

        public void Deselect()
        {
            OnDeselected?.Invoke();
        }
    }
}