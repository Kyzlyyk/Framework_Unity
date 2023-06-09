using Kyzlyk.Helpers;
using System.Linq;
using UnityEngine;
using Kyzlyk.Helpers.Utils;
using Kyzlyk.Helpers.Extensions;

namespace Kyzlyk.LifeSupportModules.DynamicFX.PresetComposing
{
    public class PresetLinker : MonoBehaviour
    {
        [SerializeField] private LayerMask _maskPresets;

        private IPreseter[] _preseters;

        private void Awake()
        {
            _preseters = GLOBAL_CONSTANTS.AllScriptsOnScene.OfType<IPreseter>().ToArray();
        }

        public void ApplyAllPresets(PresetStyle presetStyle) => _preseters.ForEach(p => p.ApplyPreset(presetStyle));
        public void ApplyMaskPresets(PresetStyle presetStyle)
        {
            _preseters.ForEach(p =>
            {
                if (UnityUtility.IsLayerIncluded(_maskPresets, p.Layer))
                    p.ApplyPreset(presetStyle);
            });
        }
    }
}