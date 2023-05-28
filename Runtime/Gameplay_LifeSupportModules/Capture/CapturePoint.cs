using System;
using Kyzlyk.AI;
using UnityEngine;
using System.Linq;
using Kyzlyk.Helpers;
using JetBrains.Annotations;
using System.Collections.Generic;
using Kyzlyk.LifeSupportModules.Player

namespace Gameplay.Modes.Capture
{
    //if you don't want to be tied to the Player component,
    //then change the Player component to the ICapturer interface with the 'ID' and 'Group' properties
    //or use another way to check the equality of groups and objects.

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

            int playerDifference = _capturerTeams[largetGroupIndex].Capturers.Count;

            for (int i = 0; i < _capturerTeams.Count; i++)
            {
                if (i == largetGroupIndex) continue;

                playerDifference -= _capturerTeams[i].Capturers.Count;
            }

            return ((playerDifference * _percentageIncreaser), largestGroup);
        }

        private void DetectionArea_EntityEntered(Collider2D[] before, Collider2D[] after)
        {
            Player target = FindUniqeCapturer(after, before);

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
            Player target = FindUniqeCapturer(after, before);

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
        private Player FindUniqeCapturer(Collider2D[] first, Collider2D[] second)
        {
            if (first.Length > second.Length)
                return first.Except(second, new PlayerEqualityComparer()).FirstOrDefault().gameObject.GetComponent<Player>();
            
            else if (second.Length > first.Length)
                return second.Except(first, new PlayerEqualityComparer()).FirstOrDefault().gameObject.GetComponent<Player>();

            return null;
        }

        private void Capture()
        {
            PointCaptured?.Invoke(this, new PointOwnerEventArgs(PointOwnerGroup, _capturerTeams.GetValue(PointOwnerGroup).Capturers));
        }

        private class Team
        {
            public List<Player> Capturers { get; } = new List<Player>(2);

            public bool TryAddCapturer(Player capturer)
            {
                if (Capturers.Any(c => c.ID == capturer.ID)) return false;
                
                Capturers.Add(capturer);
                return true;
            }
        }

        private struct PlayerEqualityComparer : IEqualityComparer<Collider2D>
        {
            public bool Equals(Collider2D x, Collider2D y)
            {
                return x.gameObject.GetComponent<Player>().ID == y.gameObject.GetComponent<Player>().ID;
            }

            public int GetHashCode(Collider2D obj)
            {
                return obj.gameObject.GetComponent<Player>().ID;
            }
        }

        public class PointOwnerEventArgs : EventArgs
        {
            public PointOwnerEventArgs(char group, IEnumerable<Player> capturers)
            {
                Group = group;
                Capturers = capturers;
            }

            public char Group { get; }
            public IEnumerable<Player> Capturers { get; }
        }
    }
}