using System;
using UnityEngine;
using System.Collections.Generic;
using Gameplay;

namespace Kyzlyyk.Backgrounds
{
    public sealed class StyleLinker : MonoBehaviour, IObservable<StyleInfo>
    {
        private readonly List<IObserver<StyleInfo>> _listeners = new(1);

        public void ComposeStyle()
        {
            //Choose the style.
            BackgroundStyle style = BackgroundStyle.Sunday;

            StyleInfo styleInfo = new(style);
            
            for (int i = 0; i < _listeners.Count; i++)
            {
                _listeners[i].OnNext(styleInfo);
            }
        }

        public IDisposable Subscribe(IObserver<StyleInfo> observer)
        {
            _listeners.Add(observer);
            return null;
        }
    }
}