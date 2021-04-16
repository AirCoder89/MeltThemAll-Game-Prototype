
using Core;
using UnityEngine;

namespace Actors.Destructible
{
    public class SpawningArea : BaseMonoBehaviour
    {
        public EnemyBase enemy;
        public float itemXSpread = 10;
        public float itemYSpread = 0;
        public float itemZSpread = 10;
        
        protected override void ReleaseReferences()
        {
            enemy = null;
        }

        public void Generate(int inAmount)
        {
            for (var i = 0; i < inAmount; i++)
            {
                var randPosition = new Vector3(
                    Random.Range(-itemXSpread, itemXSpread), 
                    Random.Range(-itemYSpread, itemYSpread), 
                    Random.Range(-itemZSpread, itemZSpread)) + transform.position;
            
                var newEnemy = GetNewEnemy(randPosition);
                newEnemy.Initialize();
            }
        }
        
        private EnemyBase GetNewEnemy(Vector3 inPosition)
        {
            var e = Instantiate(enemy, inPosition, Quaternion.identity);
            return e;
        }
    }
}