namespace Kyzlyk.GSystems.UI_Building
{
    public interface IChildrenElement
    {
        Element Parent { get; }
        bool TrySetParent(Element parent);
    }
    
    public interface IChildrenElement<TParent> : IChildrenElement where TParent : Element
    {
        new TParent Parent { get; }
    }
}
