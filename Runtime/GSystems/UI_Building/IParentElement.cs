namespace Kyzlyk.GSystems.UI_Building
{
    public interface IParentElement
    {
        IChildrenElement[] Childrens { get; }
    }

    public interface IParentElement<TChildren> : IParentElement where TChildren : IChildrenElement
    {
        new TChildren[] Childrens { get; }
    }
}
