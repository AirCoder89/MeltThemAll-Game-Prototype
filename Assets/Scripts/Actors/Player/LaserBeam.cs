using Core;
using NaughtyAttributes;
using UnityEngine;

namespace Actors.Player
{
    public class LaserBeam : BaseMonoBehaviour
    {
        [BoxGroup("Internal References")][SerializeField][Required] 
        private LineRenderer lineRenderer;
    
        [BoxGroup("Internal References")][SerializeField][Required] 
        private ParticleSystem beamFx;
    
        [BoxGroup("Internal References")][SerializeField][Required] 
        private Transform firePoint;

        protected override void ReleaseReferences()
        {
            beamFx = null;
            firePoint = null;
            lineRenderer = null;
        }

        public void Initialize() => Stop();

        public void Fire(Vector3 inTargetPosition)
        {
            beamFx.Play();
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, inTargetPosition);
        }

        public void Stop()
        {
            beamFx.Stop();
            lineRenderer.enabled = false;
        }
    }
}
