using System.Collections.Generic;
using System.Linq;
using Core;
using Interfaces;
using UnityEngine;

namespace Actors.Destructible
{
    public class EnemiesSystem : GameSystem, ITick
    {
        public int nbEnemy;
        public SpawningArea targetArea;
        public Transform player;
        public MeltingData meltingData;
        
       private List<EnemyBase> _enemies;
        protected override void ReleaseReferences()
        {
            _enemies = null;
            targetArea = null;
            player = null;
        }

        public override void Initialize()
        {
            base.Initialize();
            EnemyBase.OnInstantiated += RegisterEnemy;
            targetArea.Generate(nbEnemy);
        }

        private void RegisterEnemy(EnemyBase inNewEnemy)
        {
            if (_enemies == null) _enemies = new List<EnemyBase>();
            _enemies.Add(inNewEnemy);
            inNewEnemy.OnMelted += OnEnemyMelted;
            inNewEnemy.BindData(player, meltingData);
        }

        private void OnEnemyMelted(EnemyBase inEnemy, Vector3 inPos)
        {
            _enemies.Remove(inEnemy);
            //todo : add vfx
            Debug.Log($"Boom !!");
            if (_enemies.Count == 0) targetArea.Generate(nbEnemy);
        }

        public void Tick(float deltaTime)
        {
            if(!isRun || _enemies == null || _enemies.Count == 0) return;
            foreach (var enemy in _enemies.ToList())
                enemy.Tick();
        }
    }
}