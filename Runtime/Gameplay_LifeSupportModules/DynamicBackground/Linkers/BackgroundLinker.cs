using System.Linq;
using UnityEngine;
using Kyzlyk.Helpers;
using Kyzlyk.LifeSupportModules.DynamicBackground.Components;

namespace Kyzlyk.LifeSupportModules.DynamicBackground.Linkers
{
    public sealed class BackgroundLinker : MonoBehaviour
    {
        [SerializeField] private BackgroundSwitcher _backgroundSwitcher;
        [SerializeField] private StyleLinker _styleLinker;
        
        private ComponentLinker _componentLinker;

        private IBackground[] _allBackgrounds;

        private void Awake()
        {
            BackgroundComponent[] allComponents = FindObjectsOfType<BackgroundComponent>(includeInactive: true);

            _allBackgrounds = allComponents.OfType<IBackground>().ToArray();

            _componentLinker = new ComponentLinker(allComponents);

            _styleLinker.Subscribe(_componentLinker);
        }

        //TODO: Background selecting logic...
        private void Update()
        {
            if (false)//(Time.time > 10)
            {
                _componentLinker.Selector = c => c is IBackground;
                IBackground background = _componentLinker.SelectedComponents.First() as IBackground;
                _backgroundSwitcher.Switch(background);
            }
        }
    }
}