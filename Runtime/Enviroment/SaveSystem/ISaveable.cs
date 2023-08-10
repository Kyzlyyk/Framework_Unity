namespace Kyzlyk.Enviroment.SaveSystem
{
    public interface ISaveable
    {
        string SaveKey { get; }
        ISaveService SaveService { get; }
    }
}