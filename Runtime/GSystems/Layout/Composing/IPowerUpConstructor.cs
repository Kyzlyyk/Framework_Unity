namespace Kyzlyk.LSGSystem.Layout.Composing
{
    public interface IPowerUpConstructor
    {
        IPowerUpConstructor AddBuffs();
        IPowerUpConstructor AddDebuffs();

        IPowerUpConstructor AddTraps();
    }
}