using UnityEngine;

namespace Kyzlyk.Helpers
{
    public readonly struct GLOBAL_CONSTANTS
    {
        #region camera settings
        public static readonly int CameraWidth = Mathf.CeilToInt(Camera.main.orthographicSize) * 4 + 1;
        public static readonly int CameraHeight = Mathf.CeilToInt(Camera.main.orthographicSize) * 2;
        public static readonly int CameraDepth = Mathf.CeilToInt(Camera.main.depth);
        #endregion
        #region layers int
        public static readonly int DecorLayerInt = LayerMask.NameToLayer(DecorLayer);
        public static readonly int PlayerLayerInt = LayerMask.NameToLayer(PlayerLayer);
        public static readonly int BackgroundLayerInt = LayerMask.NameToLayer(BackgroundLayer);
        public static readonly int EntityLayerInt = LayerMask.NameToLayer(EntityLayer);
        #endregion
        #region layers
        public const string DecorLayer = "Decor";
        public const string PlayerLayer = "Player";
        public const string BackgroundLayer = "Background";
        public const string EntityLayer = "Entity";
        #endregion
        #region scenes
        public const string SampleScene = "Sample";
        public const string MainMenuScene = "Menu";
        public const string GlobalMapScene = "GlobalMap";
        #endregion
        #region unity
        public static readonly MonoBehaviour[] AllScriptsOnScene = Object.FindObjectsOfType<MonoBehaviour>(includeInactive: true);
        #endregion
    }
}