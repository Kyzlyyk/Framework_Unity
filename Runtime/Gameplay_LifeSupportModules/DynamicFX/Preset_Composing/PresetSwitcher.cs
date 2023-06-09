using Kyzlyk.Helpers;
using System.Linq;
using UnityEngine;
using Kyzlyk.Helpers.Extensions;

namespace Kyzlyk.LifeSupportModules.DynamicFX.PresetComposing
{
    [RequireComponent(typeof(PresetLinker))]
    internal sealed class PresetSwitcher : MonoBehaviour
    {
        private PresetLinker _presetLinker;
        private IPresetStyleChanger[] _styleChangers;

        private void Awake()
        {
            _presetLinker = GetComponent<PresetLinker>();

            _styleChangers = GLOBAL_CONSTANTS.AllScriptsOnScene.OfType<IPresetStyleChanger>().ToArray();
            _styleChangers.ForEach(sc => sc.OnStyleChange += OnStyleChange);
        }

        private void OnStyleChange(PresetStyle style)
        {
            _presetLinker.ApplyMaskPresets(style);
        }
    }
}