using Kyzlyk.Helpers.Extensions;
using UnityEngine;

namespace Kyzlyk.Enviroment.UI
{
    public class SwitcherGroup : MonoBehaviour
    {
        private Switcher[] _switchers;

        public Switcher Selected { get; private set; }

        private void Awake()
        {
            _switchers = GetComponentsInChildren<Switcher>(true);
            _switchers.ForEach(s => s.gameObject.SetActive(false));
        }

        public void Switch(Switcher switcher)
        {
            if (Selected != null)
            {
                Selected.SwitchOff();
                Selected.gameObject.SetActive(false);
            }

            Selected = switcher;
            Selected.gameObject.SetActive(true);
            Selected.SwitchOn();
        }
    }
}