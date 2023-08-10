using UnityEngine;
using Kyzlyk.Helpers.Extensions;
using Kyzlyk.Core;

namespace Kyzlyk.GSystems.UI_Building
{
    public abstract class InterfaceDesigner : Singleton<InterfaceDesigner>
    {
        [SerializeField] private float _delayToTransitToHoldingState;

        private Element[] _elements;

        public delegate void TouchHanlder(object sender, Touch touch);
        public event TouchHanlder OnScreenTouched;
        public event TouchHanlder OnScreenReleased;

        private bool _pointerDown;
        private float _holdingTimer;
        
        public bool IsHolding => _holdingTimer >= _delayToTransitToHoldingState && _pointerDown;
        private Touch _lastTouch;

        private int _lastTouchCount;

        protected override bool IsPersistance => false;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_pointerDown)
            {
                _holdingTimer += Time.deltaTime;
            }

            if (Input.touchCount > 0)
            {
                _lastTouch = Input.GetTouch(0);
                if (_lastTouchCount == 0)
                {
                    _lastTouchCount = Input.touchCount;
                    OnPointerDown(_lastTouch);
                }
            }
            
            else if (Input.touchCount == 0 && _lastTouchCount > 0)
            {
                _lastTouchCount = Input.touchCount;
                OnPointerUp(_lastTouch);
            }
        }
        
        protected abstract void InitializeInterface(ref Element[] elements);

        protected void Draw()
        {
            InitializeInterface(ref _elements);

            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].Interface = this;
                _elements[i].InitRender();

                if (_elements[i] is Element element and IParentElement parentElement)
                    for (int j = 0; j < parentElement.Childrens.Length; j++)
                    {
                        if (!parentElement.Childrens[j].TrySetParent(element))
                            parentElement.Childrens.Remove(parentElement.Childrens[j]);
                    }
            }
        }

        private void OnPointerUp(Touch touch)
        {
            OnScreenReleased(this, touch);
            _holdingTimer = 0f;
            _pointerDown = false;
        }

        private void OnPointerDown(Touch touch)
        {
            OnScreenTouched(this, touch);
            _pointerDown = true;
        }

        public Vector2 GetWorldPosition(Vector2 canvasPosition)
        {
            return _camera.ScreenToWorldPoint2D(canvasPosition);
        }
        
        public Vector2 GetCanvasPosition(Vector2 worldPosition)
        {
            return _camera.WorldToScreenPoint(worldPosition);
        }

        /// <summary>
        /// Use when you want clear your preferences.
        /// </summary>
        public abstract void ReturnControl();

        public void Lock<T>() where T : Element
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (_elements[i] is T elementToLock)
                    elementToLock.Lock();
            }
        }

        public void Unlock<T>() where T : Element
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (_elements[i] is T elementToUnlock)
                    elementToUnlock.Unlock();
            }
        }

        public void LockAllExcept<T>(bool exceptChild = true) where T : Element
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (CheckToExcept<T>(_elements[i], exceptChild)) continue;

                _elements[i].Lock();
            }
        }

        public void UnlockAllExcept<T>(bool exceptChild = true) where T : Element
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (CheckToExcept<T>(_elements[i], exceptChild)) continue;

                _elements[i].Unlock();
            }
        }

        private bool CheckToExcept<T>(Element element, bool exceptChild) where T : Element
        {
            return element is T || (exceptChild && (element is IChildrenElement<T>));
        }

        public void LockAll()
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].Lock();
            }
        }

        public void UnlockAll()
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].Unlock();
            }
        }
    }
}
