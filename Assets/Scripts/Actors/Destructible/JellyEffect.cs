using Core;
using UnityEngine;

namespace Actors.Destructible
{
    public class JellyEffect : BaseMonoBehaviour
    {
        [Range(0.1f,15)] public float jellyMultiplier = 2;
        [Range(0.1f,15)] public float scaleMultiplier = 2;
        
        [SerializeField] private Material material;
        private Vector3 _startScale;
        private IDestructible _destructible;
        private static readonly int Amount = Shader.PropertyToID("_Amount");

        protected override void ReleaseReferences()
        {
            _destructible = null;
            material = null;
        }

        public void Initialize(IDestructible inParent)
        {
            _destructible = inParent;
            _startScale = transform.localScale;
            material = GetComponent<MeshRenderer>().material;
        }

        public void Tick()
        {
            if (material == null || _destructible == null) return;
            var mul = (1-_destructible.Percentage * jellyMultiplier) / 10;
            material.SetFloat(Amount,mul);
            var scale = mul * scaleMultiplier;
            transform.localScale = _startScale + new Vector3(scale, scale,scale);
        }
       
    }
}