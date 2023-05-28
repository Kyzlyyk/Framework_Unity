using Helpers;
using UnityEngine;
using System.Collections;
using GSystems.DynamicBackground;
using GSystems.DynamicBackground.Components;

namespace Kyzlyk.Gameplay.DynamicBackground.Components
{
    internal class MoonBackground : BackgroundComponent, IBackground
    {
        public MoonTransition TransitionType
        {
            set
            {
                float z = transform.position.z;

                _startPosition = value switch
                {
                    MoonTransition.FromLeftToRight => new Vector3(-GLOBAL_CONSTANTS.CameraWidth, 0, z),
                    MoonTransition.FromRightToLeft => new Vector3(GLOBAL_CONSTANTS.CameraWidth, 0, z),
                    MoonTransition.FromBottomToUp => new Vector3(0, -GLOBAL_CONSTANTS.CameraHeight, z),
                    MoonTransition.FromUpToBottom => new Vector3(0, GLOBAL_CONSTANTS.CameraHeight, z),

                    _ => Vector3.zero
                };
            }
        }

        [Header("Animation")]
        [SerializeField] private AnimationCurve _transitionCurve;
        [SerializeReference] private float _transitionDuration;
        
        [Header("Settings")]
        [SerializeField] private Vector3 Destination;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private MoonTransition _transitionType;

        private void Start()
        {
            TransitionType = _transitionType;
        }

        public IEnumerator EndTransition()
        {
            yield return PlayTransition(false, _startPosition);
        }

        public IEnumerator StartTransition()
        {
            yield return PlayTransition(true, Destination);
        }

        private IEnumerator PlayTransition(bool activationValue, Vector3 targetPosition)
        {
            gameObject.SetActive(activationValue);

            float elapsedTime = 0f;

            while (elapsedTime < _transitionDuration)
            {
                float progress = elapsedTime / _transitionDuration;

                float eval = _transitionCurve.Evaluate(progress);

                transform.position = Vector3.Lerp(transform.position, targetPosition, (elapsedTime / _transitionDuration));

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        public enum MoonTransition
        {
            FromLeftToRight = 1, FromRightToLeft = 2,
            FromBottomToUp = 3, FromUpToBottom = 4,
        }
    }
}