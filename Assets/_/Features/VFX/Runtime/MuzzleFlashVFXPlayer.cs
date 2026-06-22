using Projectile.Runtime;
using UnityEngine;

namespace VFX.Runtime
{
    public class MuzzleFlashVFXPlayer : MonoBehaviour
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private GameObject _muzzleFlashPrefab;

        private void OnEnable()
        {
            if (_shooter != null) _shooter.OnShoot += HandleShoot;
        }

        private void OnDisable()
        {
            if (_shooter != null) _shooter.OnShoot -= HandleShoot;
        }

        private void HandleShoot()
        {
            if (_muzzleFlashPrefab == null || _muzzle == null) return;
            Instantiate(_muzzleFlashPrefab, _muzzle.position, _muzzle.rotation, _muzzle);
        }
    }
}