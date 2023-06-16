namespace Kyzlyk.LSGSystem.DynamicFX.PresetComposing
{
    public delegate void StyleHandler(PresetStyle style);

    public interface IPresetStyleChanger
    {
        event StyleHandler OnStyleChange;
    }
}