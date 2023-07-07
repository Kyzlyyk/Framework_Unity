using Kyzlyk.Helpers.Utils;
using Kyzlyk.Helpers.Extensions;
using System.Linq;
using UnityEngine;

namespace Kyzlyk.GSystems.DynamicFX.PresetComposing
{
    public class PresetLinker : MonoBehaviour
    {
        [SerializeField] private LayerMask _maskPresets;

        private IPreseter[] _preseters;

        private void Awake()
        {
            _preseters = FindObjectsOfType<MonoBehaviour>(true).OfType<IPreseter>().ToArray();
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