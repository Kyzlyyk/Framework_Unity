using System;
using UnityEngine;
using System.Linq;
using Kyzlyk.Helpers;
using Kyzlyk.Helpers.Core;
using Kyzlyk.Helpers.Utils;
using Kyzlyk.Helpers.GMesh;
using System.Collections.Generic;

namespace Kyzlyk.LifeSupportModules.Breaking.Modules
{
    public sealed class GDecorator : MonoBehaviour, IWrapper
    {
        public Builder Builder { get => _builder; set => _builder = value; }

        #region inspector settings
        [Header("Components")]
        [SerializeField] private Builder _builder;
        [SerializeField] private MeshContainer<(Vector2, int)> _decor;

        [Space]
        [Header("Settings")]
        [SerializeField] private DecorationNoiseMapTemplate _decorationNoiseMap = new(new Vector3[]
        {
            new Vector3(.3f, 0f, .3f),
            new Vector3(-.1f, 0f, .1f),
            new Vector3(0f, 0f, 0f),

        }, 13, new Vector2(0f, .1f), randomizeMap: true);

        [Space]
        [Header("Items shape")]
        [SerializeField]
        private float[] _decorationScales = new float[]
        {
            .2f, .1f, .09f, .07f
        };
        #endregion

        #region unity methods
        private void Start()
        {
            _builder.Supervisor.AddWrapper(this);

            CacheMaps();
        }
        #endregion

        #region public methods
        public void WrapGMaterial(Vector2Int gMaterialPosition)
        {
            for (int i = 1; i <= Builder.GMaterialSidesCount; i++)
            {
                Vector2Int nearestGMaterial =
                    gMaterialPosition +
                    new Vector2Int(
                        (int)OrientationUtils.ToHorizontal((Direction)i),
                        (int)OrientationUtils.ToVertical((Direction)i));

                if (!Builder.HasGMaterial(nearestGMaterial))
                    SpawnSide((Direction)i, ref gMaterialPosition);
                else
                    _decor.Remove(
                        (nearestGMaterial, (int)OrientationUtils.ToOpposite((Direction)i)), false);
            }
        }

        public void UnwrapGMaterial(Vector2Int gMaterialPosition)
        {
            for (int i = 1; i <= Builder.GMaterialSidesCount; i++)
            {
                _decor.Remove((gMaterialPosition, i), false);

                Direction currentSide = (Direction)i;

                Vector2Int nearestGMaterial =
                    gMaterialPosition
                    + new Vector2Int(
                        (int)OrientationUtils.ToHorizontal(currentSide),
                        (int)OrientationUtils.ToVertical(currentSide));

                if (Builder.HasGMaterial(nearestGMaterial))
                    SpawnSide(OrientationUtils.ToOpposite(currentSide), ref nearestGMaterial);
            }
        }

        public void UnwrapAll()
        {
            _decor.Reset();
        }

        public void ApplyWrap()
        {
            _decor.Apply();
        }
        #endregion

        #region private methods
        private void CacheMaps()
        {
            for (int i = 1; i <= Builder.GMaterialSidesCount; i++)
                _decorationNoiseMap.ToCache((Direction)i);
        }

        private void SpawnSide(Direction direction, ref Vector2Int gMaterialPosition)
        {
            if (!_decorationNoiseMap.TryGetCachedValue(direction, out NoiseMap map))
                return;

            Corner SideToCorner()
            {
                return _ = direction switch
                {
                    Direction.Up => Corner.BottomLeft,
                    Direction.Right => Corner.BottomLeft,
                    Direction.Left => Corner.BottomRight,
                    Direction.Down => Corner.TopLeft,

                    _ => Corner.BottomLeft
                };
            }

            _decor.Add
            (
               DecorationConstructor.GenerateDecoration
               (
                    position: gMaterialPosition
                        + new Vector2
                    (
                        (int)OrientationUtils.ToHorizontal(direction),
                        (int)OrientationUtils.ToVertical(direction)
                    ),

                    cornerToStick: SideToCorner(),
                    scaleRange: _decorationScales,
                    dimension: Settings.Global.Dimension,

                    map: map
               ),
               key: (gMaterialPosition, (int)direction),
               apply: false
            );
        }
        #endregion

        #region internal classes
        [Serializable]
        private class DecorationNoiseMapTemplate : ICacher<NoiseMap, Direction>
        {
            public DecorationNoiseMapTemplate(Vector3[] pointMapPart, byte decorativeElementsDensity, Vector2 offsetBetweenMapsPart, bool randomizeMap)
            {
                MapPointsPart = pointMapPart ?? throw new System.NullReferenceException("Parameter 'pointMapPart' mustn't equal null!");

                PartRepeat = decorativeElementsDensity;
                RandomizeParts = randomizeMap;
                OffsetBetweenParts = offsetBetweenMapsPart;
            }

            public bool RandomizeParts;
            public byte PartRepeat;
            [Space] public Vector2 OffsetBetweenParts;
            [Space] public Vector3[] MapPointsPart;

            private readonly NoiseMap[] _cachedMaps = new NoiseMap[Builder.GMaterialSidesCount];

            public bool TryGetCachedValue(Direction side, out NoiseMap cachedMap)
            {
                if (!_cachedMaps.Any())
                {
                    cachedMap = null;
                    return false;
                }
                
                cachedMap = _cachedMaps[(int)side - 1];

                cachedMap.Reset();
                
                return true;
            }

            public NoiseMap GetValue(Direction side)
            {
                List<Vector3> result = new(MapPointsPart.Length);

                Vector3 SwitchXByY(Vector3 vector)
                {
                    return new Vector3(vector.y, vector.x, vector.z);
                }

                for (int i = 0; i < MapPointsPart.Length; i++)
                {
                    Vector3 v3 = side is Direction.Up or Direction.Down
                        ? SwitchXByY(MapPointsPart[i]) * (int)OrientationUtils.ToVertical(side)
                        : MapPointsPart[i] * (int)OrientationUtils.ToHorizontal(side);

                    result.Add(new Vector3(v3.x, v3.y, MapPointsPart[i].z));
                }

                return new NoiseMap(
                    result,
                    PartRepeat,
                    side is Direction.Up or Direction.Down
                        ? SwitchXByY(OffsetBetweenParts)
                        : OffsetBetweenParts,
                    RandomizeParts);
            }

            public void ToCache(Direction side)
            {
                _cachedMaps[(int)side - 1] = GetValue(side);
            }
        }
        #endregion
    }
}