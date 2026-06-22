using UnityEngine;
using System;

namespace Health.Runtime
{
    [RequireComponent(typeof(Health))]
    public class DeathEvent : MonoBehaviour
    {
        public event Action<GameObject> OnDeath;

        private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void OnEnable() => health.OnDeath += HandleDeath;

        private void OnDisable() => health.OnDeath -= HandleDeath;
   
        private void HandleDeath()
        {
            OnDeath?.Invoke(gameObject);
        }
    }
}
