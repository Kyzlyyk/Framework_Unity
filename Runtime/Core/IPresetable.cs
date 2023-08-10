namespace Kyzlyk.Core
{
    public interface IPresetable<TOption>
    {
        public void ApplyPreset(TOption option);
    }
}