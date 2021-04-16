using Core;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class SkyBoxHandler : GameSystem,ITick
    {
        [SerializeField] [Range(0, 1)] private float speed = 0.2f;

        protected override void ReleaseReferences() { }

        public void Tick(float deltaTime)
        {
            if(!isRun) return;
            transform.Rotate(0,180 * Time.deltaTime * this.speed,0f);
        }
    }
}
