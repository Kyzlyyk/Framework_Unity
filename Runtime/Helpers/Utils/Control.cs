using UnityEngine;

namespace Kyzlyk.Helpers.Utils
{
    public struct Control
    {
        public static void MoveToTarget(Transform toApply, Vector2 currentPosition, Vector2 targetPosition, float time)
        {
            toApply.transform.position = Vector2.Lerp(currentPosition, targetPosition, time);
        }
        
        public static void MoveToTarget(Transform toApply, Vector3 currentPosition, Vector3 targetPosition, float time)
        {
            toApply.transform.position = Vector3.Lerp(currentPosition, targetPosition, time);
        }
        
        public static void MoveToTarget(Transform toApply, Vector2 targetPosition, float time)
        {
            toApply.transform.position = Vector2.Lerp(toApply.position, targetPosition, time);
        }
        
        public static void MoveToTarget(Transform toApply, Vector3 targetPosition, float time)
        {
            toApply.transform.position = Vector3.Lerp(toApply.position, targetPosition, time);
        }
        
        public static Vector2 MoveToTarget(Vector2 currentPosition, Vector2 targetPosition, float time)
        {
            return Vector2.Lerp(currentPosition, targetPosition, time);
        }
        
        public static Vector3 MoveToTarget(Vector3 currentPosition, Vector3 targetPosition, float time)
        {
            return Vector3.Lerp(currentPosition, targetPosition, time);
        }
    }
}