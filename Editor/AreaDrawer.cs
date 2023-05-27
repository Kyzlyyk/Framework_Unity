#if UNITY_EDITOR

using Helpers;
using Helpers.Utils;
using UnityEngine;

namespace UnityEditor
{

    public class AreaDrawer : MonoBehaviour
    {
        [SerializeField] private bool _enable;

        [Space]
        [SerializeField] private CircleOrRectangle _areaShape;
        [SerializeField] private Vector2 _size;

        public Vector2 Size => _size;
        public float Radius => MathUtility.ToRadius(Size);

        private void OnDrawGizmos()
        {
            if (!_enable) return;

            Color color = Color.yellow;
            color.a = .4f;
            Gizmos.color = color;

            if (_areaShape == CircleOrRectangle.Circle)
                Gizmos.DrawSphere(transform.position, Radius);

            else if (_areaShape == CircleOrRectangle.Rectangle)
                Gizmos.DrawCube(transform.position, Size);
        }
    }
}

#endif