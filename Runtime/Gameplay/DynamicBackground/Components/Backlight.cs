using UnityEngine;
using Helpers.Core;
using GSystems.DynamicBackground;
using GSystems.DynamicBackground.Components;

namespace Kyzlyk.Gameplay.DynamicBackground.Components
{
    public sealed class Backlight : BackgroundComponent, IPresetable<BackgroundStyle>
    {
        [SerializeField] private PresetWrapper[] _wrappers;

        public void ApplyPreset(BackgroundStyle style)
        {
            PresetHelper.ApplyPresetsWithStyle(_wrappers, gameObject, style);
        }
    }
}