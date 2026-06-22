using Health.Runtime;
using UnityEngine;

namespace VFX.Runtime
{
    [RequireComponent(typeof(Health.Runtime.Health))]
    public class DeathVFXPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _deathVFXPrefab;

        private Health.Runtime.Health _health;

        private void Awake()
        {
            _health = GetComponent<Health.Runtime.Health>();
        }

        private void OnEnable()
        {
            _health.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            _health.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            if (_deathVFXPrefab == null) return;
            Instantiate(_deathVFXPrefab, transform.position, Quaternion.identity);
        }
    }
}