using System.Collections;
using UnityEngine;

namespace Kyzlyk.Helpers.Core
{
    [RequireComponent(typeof(Camera))]
    public class CameraShaker : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

      
    }
}
