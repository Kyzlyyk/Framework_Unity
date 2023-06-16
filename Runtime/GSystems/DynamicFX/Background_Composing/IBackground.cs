using System.Collections;

namespace Kyzlyk.LSGSystem.DynamicFX.BackgroundComposing
{
    public interface IBackground
    {
        IEnumerator StartTransition();
        IEnumerator EndTransition();
    }
}