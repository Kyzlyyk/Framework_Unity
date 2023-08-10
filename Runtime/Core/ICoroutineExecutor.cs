using System.Collections;
using UnityEngine;

namespace Kyzlyk.Core
{
    public interface ICoroutineExecutor
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine coroutine);
    }
}
