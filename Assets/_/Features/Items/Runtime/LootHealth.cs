using System;
using Health.Runtime;
using Interactable.Runtime;
using UnityEngine;

namespace Items.Runtime
{
    [RequireComponent(typeof(Interactable.Runtime.Interactable))]
    public class LootHealth : MonoBehaviour
    {
        [SerializeField] private int _healAmount = 25;

        public event Action OnHealApplied;

        private Interactable.Runtime.Interactable _interactable;

        private void Awake()
        {
            _interactable = GetComponent<Interactable.Runtime.Interactable>();
        }

        private void OnEnable()
        {
            _interactable.OnInteract += HandlePickup;
        }

        private void OnDisable()
        {
            _interactable.OnInteract -= HandlePickup;
        }

        private void HandlePickup(GameObject interactor)
        {
            Health.Runtime.Health health = interactor.GetComponent<Health.Runtime.Health>();
            if (health == null) return;
            if (health.CurrentHealth >= health.MaxHealth) return;

            health.Heal(_healAmount);
            OnHealApplied?.Invoke();
            Destroy(gameObject);
        }
    }
}