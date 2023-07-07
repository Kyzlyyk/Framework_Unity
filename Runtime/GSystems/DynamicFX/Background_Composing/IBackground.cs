using System.Collections;

namespace Kyzlyk.GSystems.DynamicFX.BackgroundComposing
{
    public interface IBackground
    {
        IEnumerator StartTransition();
        IEnumerator EndTransition();
    }
}