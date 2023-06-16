using UnityEngine;
using System.Collections.Generic;

namespace Kyzlyk.Enviroment.UI
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private Sprite _tabIdle;
        [SerializeField] private Sprite _tabHover;
        [SerializeField] private Sprite _tabActive;

        public TabButton SelectedTab { get; private set; }

        private List<TabButton> _tabButtons = new();

        private void Awake()
        {
            GetComponentsInChildren(true, _tabButtons);
        }

        public void Subscribe(TabButton button)
        {
            _tabButtons ??= new List<TabButton>();

            _tabButtons.Add(button);
        }

        public void OnTabHover(TabButton button)
        {
            ResetTabs();
            
            if (button != SelectedTab)
                button.Background.sprite = _tabHover;
        }

        public void OnTabHoverExit(TabButton button)
        {
            ResetTabs();

            button.Background.sprite = _tabIdle;
        }

        public void OnTabSelected(TabButton button)
        {
            if (SelectedTab != null)
                SelectedTab.Deselect();

            ResetTabs();

            SelectedTab = button;
            SelectedTab.Select();
            SelectedTab.Background.sprite = _tabActive;
        }

        public void ResetTabs()
        {
            for (int i = 0; i < _tabButtons.Count; i++)
            {
                if (_tabButtons[i] == SelectedTab && SelectedTab != null) continue;

                _tabButtons[i].Background.sprite = _tabIdle;
            }
        }
    }
}