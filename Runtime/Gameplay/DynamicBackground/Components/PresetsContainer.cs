using UnityEngine;
using System.Linq;
using Helpers.Core;
using GSystems.DynamicBackground;

namespace Kyzlyk.Gameplay.DynamicBackground.Components
{
    public class PresetsContainer : MonoBehaviour, IPresetable<BackgroundStyle>
    {
        [SerializeField] private bool _dimensionalHandling;
        [Space][SerializeField] private PresetWrapper[] _presets_Global;

        public void ApplyPreset(BackgroundStyle option)
        {
            if (Settings.Global.Dimension == Dimension.TwoD)
                PresetHelper.ApplyPresetsWithStyle(_presets_2D.Concat(_presets_Global).ToArray(), gameObject, option);
            
            else if (Settings.Global.Dimension == Dimension.ThreeD)
                PresetHelper.ApplyPresetsWithStyle(_presets_3D.Concat(_presets_Global).ToArray(), gameObject, option);
        }
    }
}