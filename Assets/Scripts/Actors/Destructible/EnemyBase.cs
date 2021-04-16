using System;
using Core;
using NaughtyAttributes;
using UI.EntityCanvas;
using UnityEngine;

namespace Actors.Destructible
{
    [Serializable]
    public struct EnemyData
    {
        public float speed;
        public float damage;
        public float maxHealth;
    }
    
    
    //- List of all Vfx
    public enum DestructibleEffect
    {
        Small, Medium, Big
    }
    
    public abstract class EnemyBase : BaseMonoBehaviour, IDestructible
    {
        public static event Action<IDestructible> OnAttached;
        public static event Action<IDestructible> OnDetached;
        public static event Action<EnemyBase> OnInstantiated; 
        public event Action<EnemyBase, float> OnAttack; 
        public event Action<IDestructible, Vector3> OnTakeDamage; 
        public event Action<EnemyBase, Vector3> OnMelted;

        [BoxGroup("Establishing:")] [SerializeField]private EnemyData data;
        
        [BoxGroup("On Destroyed:")][SerializeField]
        private DestructibleEffect effect;
        public DestructibleEffect Effect => effect;
        public bool IsMelted { get; private set; }
        public float Percentage => Health / MaxHealth;

        private float _hp;
        public float Health
        {
            get => _hp;
            private set
            {
                _hp = Mathf.Clamp(value, 0, data.maxHealth);
                if (_hp > 0f) return;
                Melt();
            }
        }
        
        public float MaxHealth => data.maxHealth;
        public GameObject GetGameObject => gameObject;
        private Transform _target;
        private MeltController _melting;
        private bool _hasToMove;
        protected HealthBar _healthBar;
        protected override void ReleaseReferences()
        {
            _healthBar = null;
            _target = null;
            _melting = null;
            RemoveAllEventsListeners();
        }

        public void Initialize()
        {
            OnInstantiated?.Invoke(this);
        }

        private void OnEnable() => OnAttached?.Invoke(this);
        private void OnDisable() => OnDetached?.Invoke(this);
        
        public void BindData(Transform inTarget, MeltingData inMeltingData)
        {
            _melting = new MeltController(this, inMeltingData);
            _target = inTarget;
            _hasToMove = true;
            IsMelted = false;
            Health = MaxHealth;
        }

        public virtual void Tick()
        {
            if(IsMelted) return;
            _melting.Tick();
            MoveTowardsTarget();
        }
 
        private void MoveTowardsTarget()
        {
            if(_target == null || !_hasToMove) return;
            var step =  data.speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
            if (Vector3.Distance(transform.position, _target.position) < GameController.Parameters.distanceToAttack)
            {
                _hasToMove = false;
                OnAttack?.Invoke(this, data.damage);
                //- Todo: Attack action
                Debug.Log($"Enemy Attack !!");
            }
        }
        
        public virtual void ApplyDamage(float inDamage)
        {
           if(IsMelted) return;
               Health -= inDamage;
               OnTakeDamage?.Invoke(this, transform.position);
        }

        private void Melt()
        {
            if(IsMelted) return;
                _melting.Melt();
                IsMelted = true;
                OnMelted?.Invoke(this, transform.position);
                DestroyObject();
        }

        public void RemoveAllEventsListeners()
        {
            OnAttack = null;
            OnTakeDamage = null;
            OnMelted = null;
        }

        public void DestroyObject()
        {
            OnDetached?.Invoke(this);
            Destroy(gameObject);
        }

        public void AssignHealthBar(HealthBar inHealthBar)
        {
            _healthBar = inHealthBar;
        }
    }
}