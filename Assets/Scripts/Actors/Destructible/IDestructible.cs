using System;
using UI.EntityCanvas;
using UnityEngine;

namespace Actors.Destructible
{
    public interface IDestructible
    {
        event Action<IDestructible, Vector3> OnTakeDamage; 
        event Action<EnemyBase, Vector3> OnMelted; 
        
        DestructibleEffect Effect { get; }
        bool IsMelted { get; }
        float Percentage { get; }//between 0..1
        float Health { get; }
        float MaxHealth { get; }
        GameObject GetGameObject { get; }
        void ApplyDamage(float inDamage);
        void RemoveAllEventsListeners();
        void DestroyObject();
        void AssignHealthBar(HealthBar inHealthBar);
    }
}