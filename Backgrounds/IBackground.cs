using System.Collections;

namespace Kyzlyyk.Backgrounds
{
    internal interface IBackground
    {
        IEnumerator StartTransition();
        IEnumerator EndTransition();
    }
}