using Common.Runtime;
using UnityEngine;

namespace Enemy.Runtime
{
    public class RangedEnemyBrain : EnemyBrain
    {
        [Header("Ranged Attack")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private float _projectileSpeed = 8f;
        [SerializeField] private float _kiteDistance = 4f;

        private float _attackTimer;

        protected override void Awake()
        {
            base.Awake();
            _attackTimer = _attackCooldown;
        }

        protected override void HandleChase(float deltaTime)
        {
            if (_target == null) return;

            Vector2 toPlayer = (Vector2)_target.position - (Vector2)transform.position;
            float distance = toPlayer.magnitude;
            Vector2 direction = toPlayer.normalized;

            if (distance < _kiteDistance)
            {
                _mover.SetDirection(-direction);
            }
            else
            {
                _mover.SetDirection(direction);
            }
        }

        protected override void HandleAttack(float deltaTime)
        {
            if (_target == null) return;

            Vector2 toPlayer = (Vector2)_target.position - (Vector2)transform.position;
            Vector2 direction = toPlayer.normalized;

            _mover.SetDirection(Vector2.zero);

            _attackTimer -= deltaTime;
            if (_attackTimer <= 0f)
            {
                FireProjectile(direction);
                _attackTimer = _attackCooldown;
            }
        }

        private void FireProjectile(Vector2 direction)
        {
            if (_projectilePrefab == null) return;
            if (_muzzle == null) return;

            GameObject instance = Instantiate(_projectilePrefab, _muzzle.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * _projectileSpeed;
            }
        }
    }
}