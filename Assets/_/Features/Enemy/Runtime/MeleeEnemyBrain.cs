using Health.Runtime;
using UnityEngine;

namespace Enemy.Runtime
{
    [RequireComponent(typeof(MeleeContactDetector))]
    public class MeleeEnemyBrain : EnemyBrain
    {
        [Header("Melee Attack")]
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _attackCooldown = 1f;

        private MeleeContactDetector _contactDetector;
        private float _attackTimer;

        protected override void Awake()
        {
            base.Awake();
            _contactDetector = GetComponent<MeleeContactDetector>();
            _attackTimer = _attackCooldown;
        }

        protected override void HandleStateTransitions()
        {
            if (_target == null)
            {
                ChangeState(EnemyState.Idle);
                return;
            }

            if (_contactDetector.HasTarget)
            {
                if (_currentState != EnemyState.Attack) ChangeState(EnemyState.Attack);
                return;
            }

            float distance = Vector2.Distance(_target.position, transform.position);
            EnemyState newState = distance <= _sightRange ? EnemyState.Chase : EnemyState.Idle;

            if (newState != _currentState)
            {
                ChangeState(newState);
            }
        }

        protected override void HandleAttack(float deltaTime)
        {
            base.HandleAttack(deltaTime);

            _attackTimer -= deltaTime;
            if (_attackTimer <= 0f)
            {
                PerformMeleeAttack();
                _attackTimer = _attackCooldown;
            }
        }

        private void PerformMeleeAttack()
        {
            Health.Runtime.Health target = _contactDetector.GetFirstTarget();
            if (target == null) return;

            target.TakeDamage(_damage);
        }
    }
}