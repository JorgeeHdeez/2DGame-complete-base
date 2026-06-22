using Items.Runtime;
using UnityEngine;

namespace VFX.Runtime
{
    [RequireComponent(typeof(LootHealth))]
    public class HealPickupVFXPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _pickupVFXPrefab;

        private LootHealth _lootHealth;

        private void Awake()
        {
            _lootHealth = GetComponent<LootHealth>();
        }

        private void OnEnable()
        {
            _lootHealth.OnHealApplied += HandleHealApplied;
        }

        private void OnDisable()
        {
            _lootHealth.OnHealApplied -= HandleHealApplied;
        }

        private void HandleHealApplied()
        {
            if (_pickupVFXPrefab == null) return;
            Instantiate(_pickupVFXPrefab, transform.position, Quaternion.identity);
        }
    }
}