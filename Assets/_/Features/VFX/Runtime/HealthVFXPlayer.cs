using Health.Runtime;
using UnityEngine;

namespace VFX.Runtime
{
    [RequireComponent(typeof(Health.Runtime.Health))]
    public class HealthVFXPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _damageVFXPrefab;
        [SerializeField] private GameObject _healVFXPrefab;
        [SerializeField] private GameObject _deathVFXPrefab;

        private Health.Runtime.Health _health;

        private void Awake()
        {
            _health = GetComponent<Health.Runtime.Health>();
        }

        private void OnEnable()
        {
            _health.OnDamaged += HandleDamaged;
            _health.OnHealed += HandleHealed;
            _health.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            _health.OnDamaged -= HandleDamaged;
            _health.OnHealed -= HandleHealed;
            _health.OnDeath -= HandleDeath;
        }

        private void HandleDamaged(int amount)
        {
            SpawnVFX(_damageVFXPrefab);
        }

        private void HandleHealed(int amount)
        {
            SpawnVFX(_healVFXPrefab);
        }

        private void HandleDeath()
        {
            SpawnVFX(_deathVFXPrefab);
        }

        private void SpawnVFX(GameObject prefab)
        {
            if (prefab == null) return;
            Instantiate(prefab, transform.position, Quaternion.identity, transform);
        }
    }
}