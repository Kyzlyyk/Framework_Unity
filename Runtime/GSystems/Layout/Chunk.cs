using UnityEngine;
using Kyzlyk.LSGSystem.Breaking;
using Kyzlyk.Helpers;
using Kyzlyk.Helpers.Utils;

namespace Kyzlyk.LSGSystem.Layout
{
    [RequireComponent(typeof(Builder))]
    public class Chunk : MonoBehaviour
    {
        [Header("Decor")]
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        [Space]
        [Header("Connector")]
        [SerializeField] private Chunk _connectedChunk;

        public Chunk ConnectedChunk 
        {
            get => _connectedChunk;

            set
            {
                _connectedChunk = value;
            }
        }
        
        public Side ActiveConnector 
        {
            get => _activeConnector; 
            set
            {
                if (_activeConnector != value)
                    _activeConnector = value;
            }
        }

        public Builder Builder { get; private set; }

        public int Width { get => _width; private set => _width = value; }
        public int Height { get => _height; private set => _height = value; }

        private Side _activeConnector;

        private void Awake()
        {
            Builder = GetComponent<Builder>();
        }

        public bool TryConnect(Side targetConnectedChunkConnector)
        {
            if (targetConnectedChunkConnector == Side.None || ConnectedChunk == null || !ConnectedChunk.TryGetConnectorPosition(targetConnectedChunkConnector, out Vector2 connectorPosition))
                return false;

            transform.position = targetConnectedChunkConnector switch
            {
                Side.Right => new Vector3(connectorPosition.x, connectorPosition.y - (Height / 2)),
                Side.Top => new Vector3(connectorPosition.x - (Width / 2), connectorPosition.y),
                Side.Left => new Vector3(connectorPosition.x + Width, connectorPosition.y - (Height / 2)),
                Side.Bottom => new Vector3(connectorPosition.x - (Width / 2), connectorPosition.y - Height),
                Side.TopRight => connectorPosition,
                Side.TopLeft => new Vector3(connectorPosition.x - Width, connectorPosition.y),
                Side.BottomRight => new Vector3(connectorPosition.x, connectorPosition.y - Height),
                Side.BottomLeft => new Vector3(connectorPosition.x - Width, connectorPosition.y - Height),
                
                _ => Vector2.zero,
            };

            ConnectedChunk.ActiveConnector = targetConnectedChunkConnector;
            ActiveConnector = OrientationUtility.ToOpposite(targetConnectedChunkConnector);
            
            return true;
        }

        public Vector2 GetConnectorPosition(Side side)
        {
            float middleX = transform.position.x + Width / 2, middleY = transform.position.y + Height / 2;
            float rightX = transform.position.x + Width, topY = transform.position.y + Height;

            return _ = side switch
            {
                Side.Left => new Vector2(0f, middleY),
                Side.Right => new Vector2(rightX, middleY),
                Side.Top => new Vector2(middleX, topY),
                Side.Bottom => new Vector2(middleX, 0f),
                Side.TopLeft => new Vector2(0f, topY),
                Side.BottomRight => new Vector2(rightX, 0f),
                Side.BottomLeft => (Vector2)transform.position,
                Side.TopRight => new Vector2(rightX, topY),
                
                _ => Vector2.zero,
            };
        }
        
        public bool TryGetConnectorPosition(Side side, out Vector2 connectorPosition)
        {
            connectorPosition = GetConnectorPosition(side);

            return ActiveConnector != side;
        }

        #region unity editor (context menu) methods
#if UNITY_EDITOR

        [ContextMenu("Set default size")]
        private void SetDefaulrSize()
        {
            Width = GLOBAL_CONSTANTS.CameraWidth;
            Height = GLOBAL_CONSTANTS.CameraHeight;
        }

        [ContextMenu("Connect to binded chunk (right)")]
        private void ConnectToChunkRight()
        {
            TryConnect(Side.Right);
        }

        [ContextMenu("Connect to binded chunk (left)")]
        private void ConnectToChunkLeft()
        {
            TryConnect(Side.Left);
        }

        [ContextMenu("Connect to binded chunk (top)")]
        private void ConnectToChunkTop()
        {
            TryConnect(Side.Top);
        }

        [ContextMenu("Connect to binded chunk (bottom)")]
        private void ConnectToChunkBottom()
        {
            TryConnect(Side.Bottom);
        }
#endif
        #endregion
    }
}