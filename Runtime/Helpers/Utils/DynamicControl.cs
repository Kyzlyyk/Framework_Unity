using UnityEngine;
using System.Collections;

namespace Kyzlyk.Helpers.Utils
{
    public struct DynamicControl
    {
        public static IEnumerator MoveToTarget(Transform toApply, Vector2 targetPosition, Clip frame)
        {
            Vector3 offsetPosition = (targetPosition - (Vector2)toApply.transform.position) / frame.Frames;

            for (int i = 0; i < frame.Frames; i++)
            {
                toApply.transform.position += offsetPosition;

                yield return new WaitForSeconds(frame.Delay);
            }
        }

        public static IEnumerator MoveToTarget(Transform toApply, Vector2 targetPosition, float speed)
        {
            const float threshold = 0.05f;

            while (Vector3.Distance(toApply.position, targetPosition) > threshold)
            {
                toApply.position = Vector3.Lerp(toApply.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            toApply.position = targetPosition;
        }

        public static IEnumerator SpinAround(Transform toApply, short times, Clip frameBetweenSpinsAround, short direction = 1)
        {
            direction /= (short)Mathf.Abs(direction);

            float offsetAngle = (360 * direction - toApply.rotation.z) / frameBetweenSpinsAround.Frames;

            for (int i = 0; i < times; i++)
            {
                for (int j = 0; j < frameBetweenSpinsAround.Frames / times; j++)
                {
                    toApply.Rotate(new Vector3(0f, 0f, offsetAngle));
                    
                    yield return new WaitForSeconds(frameBetweenSpinsAround.Delay);
                }
            }
        }
    }
}