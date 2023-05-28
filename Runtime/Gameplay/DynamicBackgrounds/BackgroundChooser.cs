using System;
using Helpers;
using UnityEngine;
using System.Linq;
using Helpers.Core;
using GSystems.DynamicBackground;

namespace Kyzlyk.Gameplay.DynamicBackground
{
    internal sealed class BackgroundChooser : MonoBehaviour
    {
        public BackgroundStyle Style;
        public Predicate<IPresetable<BackgroundStyle>> Filter;

        private IPresetable<BackgroundStyle>[] _presetableComponents;

        private void Start()
        {
            _presetableComponents = GLOBAL_CONSTANTS.AllScriptsOnScene.OfType<IPresetable<BackgroundStyle>>().ToArray();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                ChooseBackground();
        }

        public void ChooseBackground()
        {
            for (int i = 0; i < _presetableComponents.Length; i++)
            {
                if (Filter == null || Filter(_presetableComponents[i]))
                    _presetableComponents[i].ApplyPreset(Style);
            }
        }
    }
}