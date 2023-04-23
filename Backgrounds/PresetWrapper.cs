using System;
using UnityEngine;
using Kyzlyyk.Utils;
using UnityEditor.Presets;

namespace Kyzlyyk.Backgrounds
{
    [Serializable]
    public class PresetWrapper
    {
        public BackgroundStyle Style;
        public Preset[] Presets;

        public Type GetTargetPresetType(int index)
        {
            return SystemHelpers.GetUnityType(Presets[index].GetTargetFullTypeName());
        }

        public void Apply(Component component, int index)
        {
            if (index >= Presets.Length)
                return;

            Presets[index].ApplyTo(component);
        }
    }
}