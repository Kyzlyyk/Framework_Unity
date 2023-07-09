using UnityEngine;
using Kyzlyk.Helpers.Extensions;

namespace Kyzlyk.GSystems.UI_Building
{
    public class DOM : MonoBehaviour, IElementsDOM
    {
        private Element[] _dom;

        protected virtual void InitializeDOM(ref Element[] dom)
        {
            dom = new Element[0];
        }

        private void Awake()
        {
            InitializeDOM(ref _dom);

            for (int i = 0; i < _dom.Length; i++)
            {
                _dom[i].DOM = this;

                if (_dom[i] is Element element and IParentElement parentElement)
                    for (int j = 0; j < parentElement.Childrens.Length; j++)
                    {
                        if (!parentElement.Childrens[j].TrySetParent(element))
                            parentElement.Childrens.Remove(parentElement.Childrens[j]);
                    }
            }
        }

        public void ReturnControl<T>() where T : Element
        {
        }

        public void Lock<T>() where T : Element
        {
            for (int i = 0; i < _dom.Length; i++)
            {
                if (_dom[i] is T elementToLock)
                    elementToLock.Lock();
            }
        }

        public void Unlock<T>() where T : Element
        {
            for (int i = 0; i < _dom.Length; i++)
            {
                if (_dom[i] is T elementToUnlock)
                    elementToUnlock.Unlock();
            }
        }

        public void LockAllExcept<T>() where T : Element
        {
            for (int i = 0; i < _dom.Length; i++)
            {
                if (_dom[i] is T) continue;

                _dom[i].Lock();
            }
        }

        public void UnlockAllExcept<T>() where T : Element
        {
            for (int i = 0; i < _dom.Length; i++)
            {
                if (_dom[i] is T) continue;

                _dom[i].Unlock();
            }
        }

        public void LockAll()
        {
            for (int i = 0; i < _dom.Length; i++)
            {
                _dom[i].Lock();
            }
        }

        public void UnlockAll()
        {
            for (int i = 0; i < _dom.Length; i++)
            {
                _dom[i].Unlock();
            }
        }
    }
}
