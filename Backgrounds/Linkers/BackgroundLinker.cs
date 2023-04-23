using System.Linq;
using UnityEngine;

namespace Kyzlyyk.Backgrounds
{
    public sealed class BackgroundLinker : MonoBehaviour
    {
        [SerializeField] private BackgroundSwitcher _backgroundSwitcher;
        [SerializeField] private StyleLinker _styleLinker;
        
        private ComponentLinker _componentLinker;

        private void Awake()
        {
            BackgroundComponent[] allComponents = FindObjectsOfType<BackgroundComponent>(includeInactive: true);

            _componentLinker = new ComponentLinker(allComponents);

            _styleLinker.Subscribe(_componentLinker);
        }

        //Background selecting logic...
        private void Update()
        {
            if (false)
            {
                _componentLinker.Selector = c => c is IBackground;
                IBackground background = _componentLinker.SelectedComponents.First() as IBackground;
                _backgroundSwitcher.Switch(background);
            }
        }
    }
}