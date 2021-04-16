
using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class SystemController
    {
        private Dictionary<Type, GameSystem> _systems;

        public SystemController()
        {
            _systems = new Dictionary<Type, GameSystem>();
        }

        public void Tick(float deltaTime)
        {
            foreach (var system in _systems)
                if(system.Value is ITick tick) tick.Tick(deltaTime);
        }
        
        public void Assign()
        {
            foreach (var gameSystem in _systems)
                if(gameSystem.Value is IAssignable assignable) assignable.Assign();
        }

        public void RemoveSystem<T>()
        {
            if(HasSystem<T>()) return;
            _systems.Remove(typeof(T));
        }
        
        public void AddSystem<T>(T inSystem) where T : GameSystem
        {
            if (HasSystem(inSystem))
            {
                Debug.LogWarning($"System Rejected : {inSystem.GetType().Name}");
                return;
            }
            _systems.Add(inSystem.GetType(), inSystem);
        }

        public bool HasSystem<T>(T inSystem)
        {
            if (_systems == null || _systems.Count == 0) return false;
            return _systems.ContainsKey(inSystem.GetType());
        }
        
        public bool HasSystem<T>()
        {
            if (_systems == null || _systems.Count == 0) return false;
            return _systems.ContainsKey(typeof(T));
        }
        
        public T GetSystem<T>() where T : GameSystem
        {
            if (!HasSystem<T>()) return null;
            return (T) _systems[typeof(T)];
        }

        public void Start()
        {
            foreach (var system in _systems)
                system.Value.StartSystem();
        }

        public void Pause()
        {
            foreach (var system in _systems)
                system.Value.PauseSystem();
        }

        public void Reset()
        {
            foreach (var system in _systems)
                system.Value.ResetSystem();
        }
    }
}