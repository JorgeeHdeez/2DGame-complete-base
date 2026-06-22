using Health.Runtime;
using UnityEngine;

namespace Enemy.Runtime
{
    [RequireComponent(typeof(global::Health.Runtime.Health))]
    public class ReleaseOnDeath : MonoBehaviour
    {
        private EnemyManager _enemyManager;
        private global::Health.Runtime.Health _health;

        private void Awake()
        {
            _health = GetComponent<global::Health.Runtime.Health>();
        }

        private void OnEnable()
        {
            _health.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            _health.OnDeath -= HandleDeath;
        }

        public void SetManager(EnemyManager manager)
        {
            _enemyManager = manager;
        }

        private void HandleDeath()
        {
            if (_enemyManager != null)
            {
                _enemyManager.ReleaseEnemy(gameObject);
            }
        }
    }
}