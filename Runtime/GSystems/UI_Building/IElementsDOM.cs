namespace Kyzlyk.GSystems.UI_Building
{
    public interface IElementsDOM
    {
        void ReturnControl<T>() where T : Element;
        void LockAll();
        void UnlockAll();
        void UnlockAllExcept<T>() where T : Element;
        void LockAllExcept<T>() where T : Element;
        public void Lock<T>() where T : Element;
        public void Unlock<T>() where T : Element;
    }
}
