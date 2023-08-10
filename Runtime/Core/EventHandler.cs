using System;

namespace Kyzlyk.Core
{
    public delegate void EventHandler<TSender, TArgs>(TSender sender, TArgs args) where TArgs : EventArgs;
    public delegate void GEventHandler<TSender>(TSender sender, EventArgs args);
}
