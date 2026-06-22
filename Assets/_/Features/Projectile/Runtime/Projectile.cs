using Health.Runtime;
using Managers.Runtime;
using UnityEngine;

namespace Projectile.Runtime
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour, IUpdatable
    {
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _lifetime = 3f;

        private UpdateManager _updateManager;
        private Rigidbody2D _rb;
        private Damager _damager;
        private float _timer;
        private bool _isRegistered;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _damager = GetComponent<Damager>();
        }

        private void OnEnable()
        {
            _timer = 0f;
            TryRegister();
        }

        private void OnDisable()
        {
            TryUnregister();
        }

        public void OnTick(float deltaTime)
        {
            HandleLifetime(deltaTime);
        }

        private void HandleLifetime(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer >= _lifetime)
            {
                Destroy(gameObject);
            }
        }

        public void Launch(Vector2 direction)
        {
            _rb.linearVelocity = direction.normalized * _speed;
        }

        public void SetSpeed(float newSpeed)
        {
            _speed = newSpeed;
        }

        public void SetDamage(int damage)
        {
            if (_damager != null)
            {
                _damager.SetDamage(damage);
            }
        }

        public void SetUpdateManager(UpdateManager manager)
        {
            _updateManager = manager;
            TryRegister();
        }

        private void TryRegister()
        {
            if (_isRegistered) return;
            if (_updateManager == null) return;

            _updateManager.Register(this);
            _isRegistered = true;
        }

        private void TryUnregister()
        {
            if (!_isRegistered) return;
            if (_updateManager == null) return;

            _updateManager.Unregister(this);
            _isRegistered = false;
        }
    }
}