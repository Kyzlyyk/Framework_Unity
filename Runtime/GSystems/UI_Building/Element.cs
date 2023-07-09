using UnityEngine;

namespace Kyzlyk.GSystems.UI_Building
{
    public abstract class Element : MonoBehaviour
    {
        private IUserInterfaceDesigner _interface;
        internal protected IUserInterfaceDesigner Interface
        {
            get => _interface;
            set => _interface ??= value;
        }

        public abstract void InitRender();
        public abstract void Lock();
        public abstract void Unlock();
    }
}