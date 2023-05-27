using System;
using UnityEngine;
using Kyzlyk.Helpers.Utils;
using UnityEditor.Presets;

namespace Kyzlyk.LifeSupportModules.DynamicBackground
{
    [Serializable]
    public class PresetWrapper
    {
        public BackgroundStyle Style;
        public Preset[] Presets;

        public Type GetTargetPresetType(int index)
        {
            return UnityUtils.GetUnityType(Presets[index].GetTargetFullTypeName());
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