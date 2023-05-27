using System;
using UnityEngine;
using System.Collections.Generic;

namespace Kyzlyk.LifeSupportModules.DynamicBackground.Linkers
{
    #region SAMPLE
    //public sealed class StyleLinker : MonoBehaviour, IBackgroundMapper, ISoundMapper, IEffectsMapper, IObservable<StyleInfo>
    //{
    //    public IEffectsMapper.Level Brightness { get; set; }
    //    public ISoundMapper.SoundFlags Sound { get; set; }
    //    public IBackgroundMapper.RangeTone Tone { get; set; }

    //    private readonly List<IObserver<StyleInfo>> _listeners = new(1);

    //    public void ComposeStyle()
    //    {
    //        //TODO
    //        BackgroundStyle style = Tone switch
    //        {
    //            IBackgroundMapper.RangeTone.Dark => BackgroundStyle.Knight,
    //            IBackgroundMapper.RangeTone.Light => BackgroundStyle.Sunday,
    //            IBackgroundMapper.RangeTone.Bright => BackgroundStyle.Japan,

    //            _ => 0
    //        };

    //        StyleInfo styleInfo = new(style);

    //        for (int i = 0; i < _listeners.Count; i++)
    //        {
    //            _listeners[i].OnNext(styleInfo);
    //        }
    //    }

    //    public IDisposable Subscribe(IObserver<StyleInfo> observer)
    //    {
    //        _listeners.Add(observer);
    //        return null;
    //    }

    //    void IMapper.OnChangeMap()
    //    {
    //         ComposeStyle();
    //    }        
    //}
    #endregion
}