using System.Collections;
using System.Collections.Generic;
using Core;
using Models;
using UnityEngine;

namespace Actors.Player
{
    public enum TreeStates
    {
        Idle, Aim
    }
    
    public class PlayerAnimController
    {
        private readonly Animator _animator;
        private readonly Camera _camera;
        private readonly PlayerSettings _settings;
        private readonly Dictionary<TreeStates, AnimationData> _treeMap;
        
        private AnimationData? _currentState;
        private AnimationData? _targetState;
        private bool _isInterpolated;
        private float _counter;
        
        public PlayerAnimController(Animator inAnimator, Camera inCamera, PlayerSettings inSettings)
        {
            _isInterpolated = false;
            _settings = inSettings;
            _animator = inAnimator;
            _camera = inCamera;
            _treeMap = new Dictionary<TreeStates, AnimationData>();
            _settings.animations.ForEach(data =>
            {
                _treeMap.Add(data.state, data);
            });
            SetState(TreeStates.Idle); //set idle animation as default.
        }
        
        public void SetAnimation(TreeStates inState)
        {
            if(inState == _currentState?.state || inState == _targetState?.state) return;
            _targetState = _treeMap[inState];
            _counter = 0f;
            _isInterpolated = true;
        }
        
        public void Tick(float deltaTime)
        {
            if(!_isInterpolated || !_targetState.HasValue) return;
            _counter += deltaTime;
            if (_counter >= _targetState.Value.speed)
            {
                //- animation completed.
                _counter = 0f;
                _isInterpolated = false;
                SetState(_targetState.Value.state);
            }
            else
            {
                //- interpolation from current to the target.
                var time = _counter / _targetState.Value.speed;
                var blendValue = Mathf.Lerp(_currentState.Value.targetValue, _targetState.Value.targetValue, time);
                _animator.SetFloat( _settings.aimVariableName, blendValue);
                _camera.fieldOfView = Mathf.Lerp( _currentState.Value.cameraFov,  _targetState.Value.cameraFov, time);
            }
        }
        
        private void SetState(TreeStates inState)
        {
            _currentState = _treeMap[inState];
            _animator.SetFloat( _settings.aimVariableName, _treeMap[inState].targetValue);
            _camera.fieldOfView = _treeMap[inState].cameraFov;
        }
 
    }
}