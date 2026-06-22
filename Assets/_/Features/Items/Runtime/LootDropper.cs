using Health.Runtime;
using UnityEngine;

namespace Items.Runtime
{
    [RequireComponent(typeof(Health.Runtime.Health))]
    public class LootDropper : MonoBehaviour
    {
        [SerializeField] private LootTable _lootTable;

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
            if (_lootTable == null) return;

            if (_lootTable.TryRoll(out GameObject prefab))
            {
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
        }

        public void SetLootTable(LootTable table)
        {
            _lootTable = table;
        }
    }
}