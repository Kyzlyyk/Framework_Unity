using UnityEngine;

namespace Kyzlyk.GSystems.UI_Building
{
    public abstract class Element : MonoBehaviour
    {
        private InterfaceDesigner _interface;
        internal protected InterfaceDesigner Interface
        {
            get => _interface;
            set
            {
                if (_interface == null)
                    _interface = value;
            }
        }

        public virtual void InitRender() { }
        public abstract void Lock();
        public abstract void Unlock();
    }
}