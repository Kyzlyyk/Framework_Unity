using System;
using Kyzlyk.AI;
using UnityEngine;
using Kyzlyk.Helpers.Math;

namespace Kyzlyk.LSGSystem.InteractiveObjects.Affectors
{
    [RequireComponent(typeof(AIAreaDetector))]
    public class Magnet : MonoBehaviour
    {
        [Range(0f, 10f)]
        [SerializeField] private float _magneticForce = 1f;
        [SerializeField] private Filter _filter;
        [SerializeField] private MagneticSource _magneticSource;

        private Predicate<GameObject> _filterPredicate;
        private AIAreaDetector _areaDetector;

        private void Awake()
        {
            _areaDetector = GetComponent<AIAreaDetector>();

            if (_filter == null)
                _filterPredicate = o => true;
            else
                _filterPredicate = _filter.GetPredicate();
        }

        private void Update()
        {
            for (int i = 0; i < _areaDetector.AllDetectedEntities.Length; i++)
            {
                //if (!_filterPredicate(_areaDetector.DetectedEntities[i].gameObject)) return;

                _areaDetector.AllDetectedEntities[i].transform.position -= _magneticForce * 0.01f * GetMagneticCenter(_areaDetector.AllDetectedEntities[i].transform.position);
            }
        }

        private Vector3 GetMagneticCenter(Vector3 entityPosition)
        {
            return _ = _magneticSource switch
            {
                MagneticSource.Magnet => MathUtility.GetVector(transform.position, entityPosition),
                MagneticSource.AreaCenter => MathUtility.GetVector((Vector3)_areaDetector.Center, entityPosition),

                _ => Vector3.zero
            };
        }

        [Serializable] 
        public class Filter
        {
            [SerializeField] private GameObject _prefab;

            public Predicate<GameObject> GetPredicate()
            {
                return new Predicate<GameObject>(g => _prefab);
            }
        }

        private enum MagneticSource
        {
            Magnet = 0,
            AreaCenter = 1,
        }
    }
}