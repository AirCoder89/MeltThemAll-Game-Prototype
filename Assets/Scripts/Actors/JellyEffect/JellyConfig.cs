using System;
using UnityEngine;

namespace Actors.JellyEffect
{
    [Serializable]
    public struct JellyConfig
    {
        [Range(0,5)] public float intensity;
        [Range(0,5)] public float mass;
        [Range(0,3)] public float stiffness;
        [Range(0,2)] public float damping;
    }
}