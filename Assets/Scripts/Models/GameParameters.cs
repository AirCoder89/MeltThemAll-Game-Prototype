using Actors.JellyEffect;
using NaughtyAttributes;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(menuName = "Game/parameters")]
    public class GameParameters : ScriptableObject
    {
        [BoxGroup("Aiming")] public bool showTargetAim;
        [BoxGroup("Enemies")] public float distanceToAttack;
        [BoxGroup("Jelly Effect")]
        public JellyConfig jelly;
    }
}