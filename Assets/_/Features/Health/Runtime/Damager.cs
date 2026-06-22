using System;
using UnityEngine;

namespace Health.Runtime
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] private int _damage = 10;
        [SerializeField] private bool _destroyOnHit = false;

        public event Action<Vector3> OnHit;

        public void Apply(Health target)
        {
            if (target == null || target.IsDead) return;
            target.TakeDamage(_damage);
            OnHit?.Invoke(transform.position);

            if (_destroyOnHit)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Health target = ResolveHealth(other);
            if (target != null)
            {
                Apply(target);
            }
        }

        private Health ResolveHealth(Collider2D collider)
        {
            Health direct = collider.GetComponent<Health>();
            if (direct != null) return direct;

            HealthForwarder forwarder = collider.GetComponent<HealthForwarder>();
            if (forwarder != null) return forwarder.GetHealth();

            return null;
        }

        public void SetDamage(int newDamage)
        {
            _damage = newDamage;
        }
    }
}