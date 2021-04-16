using System;
using Core;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Actors.Player
{
    public class PlayerRotation : BaseMonoBehaviour
    {
        public event Action onFingerDown;
        public event Action onMove;
        public event Action onFingerUp;

        private bool _isInitialized;
        private Transform _cameraTransform;
        private Vector2 _lookInput;
        private float _cameraPitch;
        private int _fingerId; // allow us to identify each finger in case that we want to create multitouch game play
        private PlayerSettings _settings;

        protected override void ReleaseReferences()
        {
            onFingerUp = null;
            onMove = null;
            onFingerDown = null;
            _settings = null;
            _cameraTransform = null;
        }

        public void Initialize(PlayerSettings inSettings, Transform inCamera)
        {
            _isInitialized = true;
            _cameraTransform = inCamera;
            _settings = inSettings;
            _fingerId = -1;
        }

        public void Tick()
        {
            if (!_isInitialized) return;
            GetTouchInput();
            if (_fingerId != -1) LookAround();
        }

        private void GetTouchInput()
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                var t = Input.GetTouch(i);
                switch (t.phase)
                {
                    case TouchPhase.Began:

                        if (_fingerId == -1)
                        {
                            onFingerDown?.Invoke();
                            _fingerId = t.fingerId;
                        }

                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:

                        if (t.fingerId == _fingerId)
                        {
                            onFingerUp?.Invoke();
                            _fingerId = -1;
                        }

                        break;
                    case TouchPhase.Moved:

                        if (t.fingerId == _fingerId)
                        {
                            onMove?.Invoke();
                            _lookInput = t.deltaPosition * _settings.cameraSensitivity * Time.deltaTime;
                        }

                        break;
                    case TouchPhase.Stationary:
                        if (t.fingerId == _fingerId) _lookInput = Vector2.zero;
                        break;
                }
            }
        }

        public void Stop()
        {
            _lookInput = Vector2.zero;
        }

        private void LookAround()
        {
            // vertical rotation
            _cameraPitch = Mathf.Clamp(_cameraPitch - _lookInput.y, -90f, 90f);
            _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);

            // horizontal rotation
            transform.Rotate(transform.up, _lookInput.x);
        }
    }
}