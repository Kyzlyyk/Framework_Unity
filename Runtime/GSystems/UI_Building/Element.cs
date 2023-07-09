using UnityEngine;

namespace Kyzlyk.GSystems.UI_Building
{
    public abstract class Element : MonoBehaviour
    {
        private IElementsDOM _dom;
        internal protected IElementsDOM DOM
        {
            get => _dom;
            set => _dom ??= value;
        }

        public abstract void InitRender();
        public abstract void Lock();
        public abstract void Unlock();
    }
}