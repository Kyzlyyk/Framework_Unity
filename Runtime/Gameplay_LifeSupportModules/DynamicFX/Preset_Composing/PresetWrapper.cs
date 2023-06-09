using System;
using UnityEngine;
using Kyzlyk.Helpers.Utils;
using UnityEditor.Presets;

namespace Kyzlyk.LifeSupportModules.DynamicFX.PresetComposing
{
    [Serializable]
    public class PresetWrapper
    {
        [SerializeField] private Preset[] _presets;
        [SerializeField] private PresetStyle _style;

        public Preset[] Presets => _presets;
        public PresetStyle Style => _style;

        public Type GetTargetPresetType(int index)
        {
            return UnityUtility.GetUnityType(Presets[index].GetTargetFullTypeName());
        }

        public void Apply(Component component, int index)
        {
            if (index >= Presets.Length)
            {
                Debug.LogAssertion(new IndexOutOfRangeException().Message);
                return;
            }

            Presets[index].ApplyTo(component);
        }
    }
}