using UnityEngine;

namespace Health.Runtime
{
    public class HealthForwarder : MonoBehaviour
    {
        [SerializeField] private Health _target;

        private void Awake()
        {
            if (_target == null)
            {
                _target = GetComponentInParent<Health>();
            }
        }

        public Health GetHealth()
        {
            return _target;
        }
    }
}
