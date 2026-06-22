using Common.Runtime;
using Managers.Runtime;
using UnityEngine;

namespace Enemy.Runtime
{
    [RequireComponent(typeof(Mover2D))]
    public class TargetFollower : MonoBehaviour, IUpdatable
    {
        [SerializeField] private Transform _target;

        private UpdateManager _updateManager;
        private Mover2D _mover;
        private bool _isRegistered;

        private void Awake()
        {
            _mover = GetComponent<Mover2D>();
        }

        private void OnEnable()
        {
            TryRegister();
        }

        private void OnDisable()
        {
            TryUnregister();
        }

        public void OnTick(float deltaTime)
        {
            HandleFollow();
        }

        private void HandleFollow()
        {
            if (_target == null)
            {
                _mover.SetDirection(Vector2.zero);
                return;
            }

            Vector2 direction = ((Vector2)_target.position - (Vector2)transform.position).normalized;
            _mover.SetDirection(direction);
        }

        public void SetTarget(Transform newTarget)
        {
            _target = newTarget;
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