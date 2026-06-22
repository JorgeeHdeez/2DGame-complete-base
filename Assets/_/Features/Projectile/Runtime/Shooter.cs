using System;
using Gun.Runtime;
using Managers.Runtime;
using UnityEngine;

namespace Projectile.Runtime
{
    public class Shooter : MonoBehaviour, IUpdatable
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private Gun.Runtime.Gun _gun;
        [SerializeField] private UpdateManager _updateManager;

        public event Action OnShoot;

        private float _cooldown;
        private float _fireRate = 5f;
        private int _damage = 10;
        private float _spread;

        private void OnEnable()
        {
            if (_updateManager != null)
            {
                _updateManager.Register(this);
            }

            if (_gun != null)
            {
                _gun.OnStatsChanged += HandleStatsChanged;
                HandleStatsChanged(_gun.CurrentStats);
            }
        }

        private void OnDisable()
        {
            if (_updateManager != null)
            {
                _updateManager.Unregister(this);
            }

            if (_gun != null)
            {
                _gun.OnStatsChanged -= HandleStatsChanged;
            }
        }

        public void OnTick(float deltaTime)
        {
            HandleCooldown(deltaTime);
        }

        private void HandleCooldown(float deltaTime)
        {
            if (_cooldown > 0f)
            {
                _cooldown -= deltaTime;
            }
        }

        private void HandleStatsChanged(GunStats stats)
        {
            _fireRate = stats.FireRate;
            _damage = stats.Damage;
            _spread = stats.Spread;
        }

        public bool TryShoot()
        {
            if (_cooldown > 0f) return false;
            if (_projectilePrefab == null || _muzzle == null) return false;

            SpawnProjectile();
            _cooldown = 1f / _fireRate;
            OnShoot?.Invoke();
            return true;
        }

        private void SpawnProjectile()
        {
            Vector2 direction = ComputeShotDirection();
            Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction);

            GameObject instance = Instantiate(_projectilePrefab, _muzzle.position, rotation);

            Projectile projectile = instance.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.SetUpdateManager(_updateManager);
                projectile.SetDamage(_damage);
                projectile.Launch(direction);
            }
        }

        private Vector2 ComputeShotDirection()
        {
            Vector2 baseDirection = _muzzle.right;
            if (_spread <= 0f) return baseDirection;

            float halfSpread = _spread * 0.5f;
            float angle = UnityEngine.Random.Range(-halfSpread, halfSpread);
            return RotateVector(baseDirection, angle);
        }

        private Vector2 RotateVector(Vector2 vector, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            float cos = Mathf.Cos(radians);
            float sin = Mathf.Sin(radians);
            return new Vector2(vector.x * cos - vector.y * sin, vector.x * sin + vector.y * cos);
        }
    }
}