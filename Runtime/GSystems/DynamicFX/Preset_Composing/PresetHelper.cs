using UnityEngine;

namespace Kyzlyk.LSGSystem.DynamicFX.PresetComposing
{
    internal struct PresetHelper
    {
        public static void ApplyPreset(PresetWrapper wrapper, GameObject applyTo)
        {
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
