using System;
using Kyzlyk.AI;
using UnityEngine;
using System.Linq;
using Kyzlyk.Helpers;
using JetBrains.Annotations;
using System.Collections.Generic;
using Kyzlyk.LifeSupportModules.ICapturer

namespace Kyzlyk.LifeSupportModules.Enhancements
{
    [RequireComponent(typeof(AIAreaDetector))]
    public class CapturePoint : MonoBehaviour
    {
        [Header("Capture Settings")]
        [SerializeField] private float _timeToIncreaseCapturingPercentageValue;
        [SerializeField][Range(1, 100)] private int _percentageIncreaser;
        
        [HideInInspector][ReadOnlyProperty][SerializeField] private int _capturedPercentage;
        [HideInInspector][ReadOnlyProperty][SerializeField] private char _pointOwnerGroup;

        [HideInInspector][SerializeField] private bool _openCaptureStatus;
            
        private readonly KeyList<char, Team> _capturerTeams = new(2);
        private AIAreaDetector _detectionArea;
        private float _timeToIncreaseCapturingPercentage;
        
        public int CapturedPercentage => _capturedPercentage;
        public char PointOwnerGroup => _pointOwnerGroup;

        public delegate void PointCaptureHandler(object sender, PointOwnerEventArgs args);
        public event PointCaptureHandler PointCaptured;

        private void Awake()
        {
            _detectionArea = GetComponent<AIAreaDetector>();
            _detectionArea.EntityEntered += DetectionArea_EntityEntered;
            _detectionArea.EntityExited += DetectionArea_EntityExited;
        }

        private void Update()
        {
            if (_detectionArea.AnyEntityDetected)
                IncreaseCapturedPercentageInCapturers();
        }

        private void IncreaseCapturedPercentageInCapturers()
        {
            if (CapturedPercentage >= 100)
            {
                return;
            }

            _timeToIncreaseCapturingPercentage += Time.deltaTime;

            if (_timeToIncreaseCapturingPercentage >= _timeToIncreaseCapturingPercentageValue)
            {
                _timeToIncreaseCapturingPercentage = 0;

                (int percentage, char largestGroup) = GetTargetPercentageAndGroup();

                if (largestGroup is '\0' or ' ') return;

                if (_capturedPercentage == 0 || _pointOwnerGroup == '\0')
                    _pointOwnerGroup = largestGroup;

                if (_pointOwnerGroup != largestGroup && percentage > 0)
                    percentage = -percentage;
                
                _capturedPercentage += percentage;

                if (CapturedPercentage >= 100)
                {
                    Capture();

                    //if (CurrentPercentage > 100)
                    _capturedPercentage = 100;
                }
            }
        }

        private (int Percentage, char Group) GetTargetPercentageAndGroup()
        {
            if (_capturerTeams.Count == 0) return (0, '\0');

            char largestGroup = _capturerTeams.GetKey(0);
            int largetGroupIndex = 0;

            for (int i = 1; i < _capturerTeams.Count; i++)
            {
                if (_capturerTeams[i].Capturers.Count > _capturerTeams[i - 1].Capturers.Count)
                {
                    largestGroup = _capturerTeams.GetKey(i);
                    largetGroupIndex = i;
                }
            }

            int capturerDifference = _capturerTeams[largetGroupIndex].Capturers.Count;

            for (int i = 0; i < _capturerTeams.Count; i++)
            {
                if (i == largetGroupIndex) continue;

                capturerDifference -= _capturerTeams[i].Capturers.Count;
            }

            return ((capturerDifference * _percentageIncreaser), largestGroup);
        }

        private void DetectionArea_EntityEntered(Collider2D[] before, Collider2D[] after)
        {
            ICapturer target = FindUniqeCapturer(after, before);

            void AddNewTeam()
            {
                var team = new Team();
                team.TryAddCapturer(target);

                _capturerTeams.Add(target.Group, team);
            }

            if (_capturerTeams.Count == 0)
            {
                AddNewTeam();
                return;
            }
            
            if (_capturerTeams.Contains(target.Group, out Team team))
                team.TryAddCapturer(target);
            else
                AddNewTeam();
        }

        private void DetectionArea_EntityExited(Collider2D[] before, Collider2D[] after)
        {
            ICapturer target = FindUniqeCapturer(after, before);

            if (_capturerTeams.Contains(target.Group, out Team team))
            {
                for (int i = 0; i < team.Capturers.Count; i++)
                {
                    if (team.Capturers[i].ID == target.ID)
                        team.Capturers.RemoveAt(i);
                }
            }
        }

        [CanBeNull]
        private ICapturer FindUniqeCapturer(Collider2D[] first, Collider2D[] second)
        {
            if (first.Length > second.Length)
                return first.Except(second, new ICapturerEqualityComparer()).FirstOrDefault().gameObject.GetComponent<ICapturer>();
            
            else if (second.Length > first.Length)
                return second.Except(first, new ICapturerEqualityComparer()).FirstOrDefault().gameObject.GetComponent<ICapturer>();

            return null;
        }

        private void Capture()
        {
            PointCaptured?.Invoke(this, new PointOwnerEventArgs(PointOwnerGroup, _capturerTeams.GetValue(PointOwnerGroup).Capturers));
        }

        private class Team
        {
            public List<ICapturer> Capturers { get; } = new List<ICapturer>(2);

            public bool TryAddCapturer(ICapturer capturer)
            {
                if (Capturers.Any(c => c.ID == capturer.ID)) return false;
                
                Capturers.Add(capturer);
                return true;
            }
        }

        private struct ICapturerEqualityComparer : IEqualityComparer<Collider2D>
        {
            public bool Equals(Collider2D x, Collider2D y)
            {
                return x.gameObject.GetComponent<ICapturer>().ID == y.gameObject.GetComponent<ICapturer>().ID;
            }

            public int GetHashCode(Collider2D obj)
            {
                return obj.gameObject.GetComponent<ICapturer>().ID;
            }
        }

        public class PointOwnerEventArgs : EventArgs
        {
            public PointOwnerEventArgs(char group, IEnumerable<ICapturer> capturers)
            {
                Group = group;
                Capturers = capturers;
            }

            public char Group { get; }
            public IEnumerable<ICapturer> Capturers { get; }
        }
    }
}