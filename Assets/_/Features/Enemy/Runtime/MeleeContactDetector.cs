using System.Collections.Generic;
using Health.Runtime;
using UnityEngine;

namespace Enemy.Runtime
{
    public class MeleeContactDetector : MonoBehaviour
    {
        [SerializeField] private Collider2D _hitboxCollider;

        private readonly HashSet<Health.Runtime.Health> _targetsInRange = new HashSet<Health.Runtime.Health>();
        private MeleeContactRelay _relay;

        public bool HasTarget => _targetsInRange.Count > 0;

        private void Awake()
        {
            if (_hitboxCollider != null)
            {
                _relay = _hitboxCollider.gameObject.AddComponent<MeleeContactRelay>();
                _relay.SetDetector(this);
            }
        }

        public Health.Runtime.Health GetFirstTarget()
        {
            foreach (Health.Runtime.Health target in _targetsInRange)
            {
                if (target != null && !target.IsDead) return target;
            }
            return null;
        }

        public void HandleEnter(Collider2D other)
        {
            Health.Runtime.Health target = ResolveHealth(other);
            if (target != null)
            {
                _targetsInRange.Add(target);
            }
        }

        public void HandleExit(Collider2D other)
        {
            Health.Runtime.Health target = ResolveHealth(other);
            if (target != null)
            {
                _targetsInRange.Remove(target);
            }
        }

        private Health.Runtime.Health ResolveHealth(Collider2D collider)
        {
            Health.Runtime.Health direct = collider.GetComponent<Health.Runtime.Health>();
            if (direct != null) return direct;

            HealthForwarder forwarder = collider.GetComponent<HealthForwarder>();
            if (forwarder != null) return forwarder.GetHealth();

            return null;
        }
    }
}