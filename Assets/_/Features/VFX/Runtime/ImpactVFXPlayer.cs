using Health.Runtime;
using UnityEngine;

namespace VFX.Runtime
{
    [RequireComponent(typeof(Damager))]
    public class ImpactVFXPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _impactVFXPrefab;

        private Damager _damager;

        private void Awake()
        {
            _damager = GetComponent<Damager>();
        }

        private void OnEnable()
        {
            _damager.OnHit += HandleHit;
        }

        private void OnDisable()
        {
            _damager.OnHit -= HandleHit;
        }

        private void HandleHit(Vector3 position)
        {
            if (_impactVFXPrefab == null) return;
            Instantiate(_impactVFXPrefab, position, Quaternion.identity);
        }
    }
}