using System.Collections;
using UnityEngine;

namespace Kyzlyyk.Backgrounds
{
    internal sealed class BackgroundSwitcher : MonoBehaviour
    {
        private IBackground _currentBackground;

        public void Switch(IBackground background)
        {
            StartCoroutine(Switch_Internal(_currentBackground, background));

            _currentBackground = background;
        }

        private IEnumerator Switch_Internal(IBackground replace, IBackground switchTo)
        {
            yield return StartCoroutine(replace.EndTransition());

            yield return StartCoroutine(switchTo.StartTransition());
        }
    }
}