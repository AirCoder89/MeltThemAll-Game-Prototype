using System.Collections.Generic;
using Actors.Destructible;
using Core;
using UI.EntityCanvas;
using UnityEngine;

namespace UI
{
    public class HealthBarsHandler : BaseMonoBehaviour
    {
        [SerializeField] private HealthBar healthBarPrefab;

        private Dictionary<IDestructible, HealthBar> _healthBars;
        protected override void ReleaseReferences()
        {
            _healthBars = null;
            healthBarPrefab = null;
            
            EnemyBase.OnAttached -= AttachHealthBar;
            EnemyBase.OnDetached -= DetachHealthBar;
        }

        private void Awake()
        {
            _healthBars = new Dictionary<IDestructible, HealthBar>();
            EnemyBase.OnAttached += AttachHealthBar;
            EnemyBase.OnDetached += DetachHealthBar;
        }
        
        private void AttachHealthBar(IDestructible inEnemy)
        {
            if(_healthBars.ContainsKey(inEnemy)) return;
            var healthBar = Instantiate(healthBarPrefab, transform);
            healthBar.Initialize();
            healthBar.BindData(inEnemy);
            inEnemy.AssignHealthBar(healthBar);
        }

        private void DetachHealthBar(IDestructible inEnemy)
        {
            if(!_healthBars.ContainsKey(inEnemy)) return;
            Destroy(_healthBars[inEnemy].gameObject);
            _healthBars.Remove(inEnemy);
        }
        
    }
}