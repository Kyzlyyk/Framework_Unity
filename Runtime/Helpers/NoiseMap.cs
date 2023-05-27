using System.Collections.Generic;
using Helpers.Extensions;
using UnityEngine;
using System.Collections;

namespace Kyzlyk.Helpers
{
    public class NoiseMap : IEnumerator<Vector3>
    {
        public NoiseMap(IReadOnlyList<Vector3> mapPointsPart, byte partsQuantity, Vector2 offsetBetweenParts, bool randomizeParts)
        {
            MapPointsPart = mapPointsPart;
            PartsQuantity = partsQuantity;
            RandomizeParts = randomizeParts;
            OffsetBetweenParts = offsetBetweenParts;
        }

        public byte PartsQuantity { get; set; }

        public Vector3 Current { get; private set; }

        public bool RandomizeParts { get; }
        public IReadOnlyList<Vector3> MapPointsPart { get; }
        public Vector2 OffsetBetweenParts { get; }

        private Vector2 _offset;
        private byte _timesRepeated;
        private int _currentIndex;
        
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_timesRepeated == PartsQuantity)
            {
                return false;
            }

            if (_currentIndex >= MapPointsPart.Count)
                _currentIndex = 0;

            _offset += OffsetBetweenParts;

            Current = RandomizeParts
                ? MapPointsPart.Random() + (Vector3)_offset
                : MapPointsPart[_currentIndex] + (Vector3)_offset;

            _currentIndex++;
            _timesRepeated++;

            return true;
        }

        public void Reset()
        {
            Current = default;
         
            _offset.Set(0, 0);
            _currentIndex = 0;
            _timesRepeated = 0;
        }

        public void Dispose()
        {
        }
    }
}