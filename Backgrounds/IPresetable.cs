namespace Kyzlyyk.Backgrounds
{
    internal interface IPresetable<TOption>
    {
        PresetWrapper[] Wrappers { get; }

        public void ApplyPreset(TOption option);
    }
}