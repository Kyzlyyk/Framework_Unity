using System;
using System.Linq;
using Kyzlyk.Helpers.Core;
using Kyzlyk.Helpers.Extensions;
using System.Collections.Generic;
using Kyzlyk.LifeSupportModules.DynamicBackground.Components;

namespace Kyzlyk.LifeSupportModules.DynamicBackground.Linkers
{
    internal class ComponentLinker : IObserver<StyleInfo>
    {
        public ComponentLinker(BackgroundComponent[] components)
        {
            Components = new List<BackgroundComponent>(components);
            _presetables = Components.OfType<IPresetable<BackgroundStyle>>().ToArray();
        }

        public Predicate<BackgroundComponent> Selector { get; set; } = c => true;
        public BackgroundComponent[] SelectedComponents { get; private set; }

        public readonly List<BackgroundComponent> Components;
        private readonly IPresetable<BackgroundStyle>[] _presetables;

        void IObserver<StyleInfo>.OnNext(StyleInfo value)
        {
            SelectedComponents = Components.Where(c =>
            {
                if (Selector != null)
                {
                    if (Selector(c))
                    {
                        c.gameObject.SetActive(true);
                        return true;
                    }

                    else if (c.gameObject.activeInHierarchy)
                        c.gameObject.SetActive(false);
                }

                return false;
            
            }).ToArray();

            ApplyPreseters(value.BackgroundStyle);
        }

        private void ApplyPreseters(BackgroundStyle style)
        {
            _presetables.ForEach(p => p.ApplyPreset(style));
        }

        void IObserver<StyleInfo>.OnCompleted()
        {
        }

        void IObserver<StyleInfo>.OnError(Exception error)
        {
        }
    }
}