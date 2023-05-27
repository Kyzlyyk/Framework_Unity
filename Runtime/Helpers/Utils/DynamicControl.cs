using UnityEngine;
using System.Collections;

namespace Kyzlyk.Helpers.Utils
{
    public struct DynamicControl
    {
        public static IEnumerator MoveToTargetByLerp(Transform toApply, Vector2 targetPosition, float time)
        {
            float elapsedTime = 0f;

            while (true)
            {
                toApply.transform.position = Vector3.Lerp(toApply.transform.position, targetPosition, (elapsedTime / time));

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        public static IEnumerator MoveToTarget(Transform toApply, Vector2 targetPosition, Clip frame)
        {
            Vector3 offsetPosition = (targetPosition - (Vector2)toApply.transform.position) / frame.Frames;

            for (int i = 0; i < frame.Frames; i++)
            {
                toApply.transform.position += offsetPosition;

                yield return new WaitForSeconds(frame.Delay);
            }
        }

        //TODO
        public static IEnumerator MoveToTarget(Transform toApply, Vector2 targetPosition, Clip frame, float overclock)
        {
            yield break;
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

        public static IEnumerator Spin(Transform toApply, float alphaAngle, float duration)
        {
            yield break;
        }

        // TODO require to complete.....
        public static IEnumerator Spin(Transform toApply, Vector3 targetAngles, float duration, short overclocking)
        {
            yield break;
        }
    }
}