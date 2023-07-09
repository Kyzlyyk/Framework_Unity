using UnityEngine;
using Kyzlyk.Helpers.Extensions;

namespace Kyzlyk.GSystems.UI_Building
{
    public class InterfaceDesigner : MonoBehaviour, IUserInterfaceDesigner
    {
        private Element[] _elements;

        protected virtual void InitializeInterface(ref Element[] elements)
        {
            elements = new Element[0];
        }

        private void Awake()
        {
            InitializeInterface(ref _elements);

            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].Interface = this;
                _elements[i].InitRender();

                if (_elements[i] is Element element and IParentElement parentElement)
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
            for (int i = 0; i < _elements.Length; i++)
            {
                if (_elements[i] is T elementToLock)
                    elementToLock.Lock();
            }
        }

        public void Unlock<T>() where T : Element
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (_elements[i] is T elementToUnlock)
                    elementToUnlock.Unlock();
            }
        }

        public void LockAllExcept<T>() where T : Element
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (_elements[i] is T) continue;

                _elements[i].Lock();
            }
        }

        public void UnlockAllExcept<T>() where T : Element
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (_elements[i] is T) continue;

                _elements[i].Unlock();
            }
        }

        public void LockAll()
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].Lock();
            }
        }

        public void UnlockAll()
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].Unlock();
            }
        }
    }
}
