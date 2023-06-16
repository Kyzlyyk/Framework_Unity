using UnityEngine;
using UnityEngine.Events;

namespace Kyzlyk.Enviroment.UI
{
    public class Switcher : MonoBehaviour
    {
        public SwitcherGroup Group;
        public UnityEvent SwitchedOn;
        public UnityEvent SwitchedOff;

        public void Switch()
        {
            Group.Switch(this);
        }

        public void SwitchOn()
        {
            SwitchedOn?.Invoke();
        }

        public void SwitchOff()
        {
            SwitchedOff?.Invoke();
        }
    }
}