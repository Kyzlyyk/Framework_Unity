using System.Collections;

namespace Kyzlyk.LifeSupportModules.DynamicBackground
{
    internal interface IBackground
    {
        IEnumerator StartTransition();
        IEnumerator EndTransition();
    }
}