using System;
using Actors.Destructible;
using UnityEngine;

namespace Actors
{
    [Serializable]
    public struct MeltingData
    {
        public Vector2 transition;
        public Vector2 meltStart;
        public Vector2 amount;
        public Vector2 multiplier;
        public Vector2 zone;
        public Vector2 scale;
    }

    [Serializable]
    public class MeltController
    {
        private readonly IDestructible _destructible;
        private static readonly int Transition = Shader.PropertyToID("_M_Trans");
        private static readonly int MeltStart = Shader.PropertyToID("_M_StartMelt");
        private static readonly int Amount = Shader.PropertyToID("_MeltAmount");
        private static readonly int Multiplier = Shader.PropertyToID("_M_Multiplier");
        private static readonly int Zone = Shader.PropertyToID("_M_Zone");

        private Material _material;
        private MeltingData? _data;
    
        public MeltController(IDestructible inParent, MeltingData inData)
        {
            _data = inData;
            _destructible = inParent;
            _material = _destructible.GetGameObject.GetComponent<MeshRenderer>().material;
        }

        public void Tick()
        {
            if (_material == null || _destructible == null || !_data.HasValue) return;
            _material.SetFloat(Transition,GetValue(1-_destructible.Percentage, _data.Value.transition.x, _data.Value.transition.y));
            _material.SetFloat(MeltStart,GetValue(1-_destructible.Percentage, _data.Value.meltStart.x, _data.Value.meltStart.y));
            _material.SetFloat(Amount,GetValue(1-_destructible.Percentage, _data.Value.amount.x, _data.Value.amount.y));
            _material.SetFloat(Multiplier,GetValue(1-_destructible.Percentage, _data.Value.multiplier.x, _data.Value.multiplier.y));
            _material.SetFloat(Zone,GetValue(1-_destructible.Percentage, _data.Value.zone.x, _data.Value.zone.y));

            var scale = GetValue(1-_destructible.Percentage, _data.Value.scale.x, _data.Value.scale.y);
            _destructible.GetGameObject.transform.localScale = new Vector3(scale, scale, scale);
        }

        public void Melt()
        {
            
        }
        
        private float GetValue(float inLevel, float inMin, float inMax)
        {
            return (inMax - Mathf.Abs(inMin)) * inLevel + inMin;
        }
    }
}