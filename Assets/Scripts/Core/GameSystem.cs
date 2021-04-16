using System;
using Actors.Player;
using UnityEngine;

namespace Core
{
    public abstract class GameSystem : BaseMonoBehaviour
    {
        public static event Action<GameSystem, Type> OnStart;
        public bool isRun;

        public virtual void Initialize() {}
       
        public virtual void StartSystem()
        {
            isRun = true;
            OnStart?.Invoke(this, GetType());
        }
        
        public virtual void PauseSystem()
        {
            isRun = false;
        }
        
        public virtual void ResetSystem() {}
    }
}