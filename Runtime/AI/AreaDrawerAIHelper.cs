#if UNITY_EDITOR

using Kyzlyk.AI;
using Kyzlyk.Helpers;
using UnityEngine;

namespace UnityEditor
{
    public class AreaDrawerAIHelper : MonoBehaviour
    {
        [SerializeField] private AIAreaDetector _areaDetector;

        private void OnDrawGizmos()
        {
            Color color = _areaDetector.AnyEntityDetected ? Color.red : Color.green;
            color.a = .4f;
            Gizmos.color = color;

            if (_areaDetector.DetectionAreaShape == CircleOrRectangle.Circle)
                Gizmos.DrawSphere(_areaDetector.Center, _areaDetector.Radius);
            
            else if (_areaDetector.DetectionAreaShape == CircleOrRectangle.Rectangle)
                Gizmos.DrawCube(_areaDetector.Center, _areaDetector.Size);
        }
    }
}

#endif