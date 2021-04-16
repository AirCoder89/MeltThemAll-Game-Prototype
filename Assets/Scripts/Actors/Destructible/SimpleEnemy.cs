

using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Actors.Destructible
{
    public class SimpleEnemy : EnemyBase
    {
        private float _counter = 0f;
        public override void Tick()
        {
            base.Tick();
            if (_healthBar == null) return;
            _counter += Time.deltaTime;
            if (_counter < 0.5f) return;
                _counter = 0f;
                var isVisible = IsVisible();
                _healthBar.SetVisibility(isVisible);
        }
        
        private bool IsVisible()
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(GameController.MainCamera);
            return planes.All(plane => !(plane.GetDistanceToPoint(transform.position) < 0));
        }
    }
}