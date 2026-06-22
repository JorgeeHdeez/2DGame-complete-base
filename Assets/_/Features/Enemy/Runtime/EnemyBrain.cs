using Common.Runtime;
using Managers.Runtime;
using UnityEngine;

namespace Enemy.Runtime
{
    [RequireComponent(typeof(Mover2D))]
    public abstract class EnemyBrain : MonoBehaviour, IUpdatable
    {
        protected enum EnemyState
        {
            Idle,
            Chase,
            Attack
        }

        [Header("Detection")]
        [SerializeField] protected float _sightRange = 10f;
        [SerializeField] protected float _attackRange = 2f;

        [Header("Target")]
        [SerializeField] protected Transform _target;

        protected EnemyState _currentState;
        protected Mover2D _mover;
        protected UpdateManager _updateManager;
        private bool _isRegistered;

        protected virtual void Awake()
        {
            _mover = GetComponent<Mover2D>();
            _currentState = EnemyState.Idle;
        }

        protected virtual void OnEnable()
        {
            TryRegister();
        }

        protected virtual void OnDisable()
        {
            TryUnregister();
        }

        public void OnTick(float deltaTime)
        {
            HandleStateTransitions();
            HandleCurrentState(deltaTime);
        }

        protected virtual void HandleStateTransitions()
        {
            if (_target == null)
            {
                ChangeState(EnemyState.Idle);
                return;
            }

            float distance = Vector2.Distance(_target.position, transform.position);
            EnemyState newState;

            if (distance <= _attackRange)
            {
                newState = EnemyState.Attack;
            }
            else if (distance <= _sightRange)
            {
                newState = EnemyState.Chase;
            }
            else
            {
                newState = EnemyState.Idle;
            }

            if (newState != _currentState)
            {
                ChangeState(newState);
            }
        }

        protected virtual void HandleCurrentState(float deltaTime)
        {
            switch (_currentState)
            {
                case EnemyState.Idle:
                    HandleIdle(deltaTime);
                    break;
                case EnemyState.Chase:
                    HandleChase(deltaTime);
                    break;
                case EnemyState.Attack:
                    HandleAttack(deltaTime);
                    break;
            }
        }

        protected virtual void HandleIdle(float deltaTime)
        {
            _mover.SetDirection(Vector2.zero);
        }

        protected virtual void HandleChase(float deltaTime)
        {
            if (_target == null) return;

            Vector2 direction = ((Vector2)_target.position - (Vector2)transform.position).normalized;
            _mover.SetDirection(direction);
        }

        protected virtual void HandleAttack(float deltaTime)
        {
            _mover.SetDirection(Vector2.zero);
        }

        protected virtual void OnEnterState(EnemyState state) { }
        protected virtual void OnExitState(EnemyState state) { }

        protected void ChangeState(EnemyState newState)
        {
            OnExitState(_currentState);
            _currentState = newState;
            OnEnterState(_currentState);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
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