using Health.Runtime;
using UnityEngine;

namespace Common.Runtime
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health.Runtime.Health))]
    public class StopOnDeath : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Health.Runtime.Health _health;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
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
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
    }
}