using UnityEngine;
using GSystems.DynamicBackground;
using System.Linq;

namespace Kyzlyk.Gameplay.DynamicBackground
{
    internal struct PresetHelper
    {
        public static void ApplyPresetsWithStyle(PresetWrapper[] wrappers, GameObject applyTo, BackgroundStyle style)
        {
            PresetWrapper wrapper = wrappers.FirstOrDefault(w => w.Style == style);

            if (wrapper == null)
            {
                Debug.LogAssertion("Wrapper is equal null! \n" + typeof(PresetHelper).FullName);
                return;
            }

            for (int i = 0; i < wrapper.Presets.Length; i++)
            {
                if (applyTo.TryGetComponent(wrapper.GetTargetPresetType(i), out var component))
                {
                    wrapper.Apply(component, i);
                }
            }
        }
    }
}
