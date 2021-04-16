using System;
using System.Collections.Generic;
using Actors.Player;
using NaughtyAttributes;
using UnityEngine;

namespace Models
{
    [Serializable]
    public struct AnimationData
    {
        public TreeStates state;
        [Range(0f,1f)] public float targetValue;
        public float cameraFov;
        public float speed;
    }
    
    [CreateAssetMenu(menuName = "Game/player settings")]
    public class PlayerSettings : ScriptableObject
    {
        [BoxGroup("Characteristics")] public float damage = 1f;
        
        [BoxGroup("Aiming")][Range(0,20)] public float cameraSensitivity = 7.5f;
        [BoxGroup("Aiming")][Range(0,500)] public float laserMaxLength;
        [BoxGroup("Aiming")][Range(0,1)] public float aimSpeed = 0.2f;
        [BoxGroup("Aiming")] public float idleFov = 60;
        [BoxGroup("Aiming")] public float aimingFov = 45;
        [BoxGroup("Aiming")] public string aimVariableName = "Aiming";
        [BoxGroup("Aiming")] public float fireRate = 0.5f;

        [BoxGroup("Animations")] public List<AnimationData> animations;
    }
}