using System;
using Gun.Runtime;
using UnityEngine;

namespace Projectile.Runtime
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField] private Gun.Runtime.Gun _gun;

        public event Action<int, int> OnAmmoChanged;
        public event Action OnReloadStarted;
        public event Action OnReloadFinished;

        private int _maxAmmo = 10;
        private int _currentAmmo;
        private float _reloadDuration = 2f;
        private bool _isReloading;
        private float _reloadTimer;

        public int CurrentAmmo => _currentAmmo;
        public int MaxAmmo => _maxAmmo;
        public bool IsReloading => _isReloading;
        
        public float ReloadProgress
        {
            get
            {
                if (!_isReloading || _reloadDuration <= 0f) return 0f;
                return 1f - (_reloadTimer / _reloadDuration);
            }
        }

        private void OnEnable()
        {
            if (_gun != null)
            {
                _gun.OnStatsChanged += HandleStatsChanged;
                HandleStatsChanged(_gun.CurrentStats);
            }
        }

        private void OnDisable()
        {
            if (_gun != null)
            {
                _gun.OnStatsChanged -= HandleStatsChanged;
            }
        }

        private void Update()
        {
            HandleReloadTimer();
        }

        private void HandleReloadTimer()
        {
            if (!_isReloading) return;

            _reloadTimer -= Time.deltaTime;
            if (_reloadTimer <= 0f)
            {
                FinishReload();
            }
        }

        private void HandleStatsChanged(GunStats stats)
        {
            bool wasUninitialized = _maxAmmo <= 0;

            _maxAmmo = stats.MagazineSize;
            _reloadDuration = stats.ReloadDuration;

            if (wasUninitialized)
            {
                _currentAmmo = _maxAmmo;
            }
            else if (_currentAmmo > _maxAmmo)
            {
                _currentAmmo = _maxAmmo;
            }

            OnAmmoChanged?.Invoke(_currentAmmo, _maxAmmo);
        }

        public bool HasAmmo()
        {
            return _currentAmmo > 0 && !_isReloading;
        }

        public void Consume()
        {
            if (_currentAmmo <= 0 || _isReloading) return;
            _currentAmmo--;
            OnAmmoChanged?.Invoke(_currentAmmo, _maxAmmo);
        }

        public void Reload()
        {
            if (_isReloading) return;
            if (_currentAmmo >= _maxAmmo) return;

            _isReloading = true;
            _reloadTimer = _reloadDuration;
            OnReloadStarted?.Invoke();
        }

        private void FinishReload()
        {
            _currentAmmo = _maxAmmo;
            _isReloading = false;
            _reloadTimer = 0f;
            OnAmmoChanged?.Invoke(_currentAmmo, _maxAmmo);
            OnReloadFinished?.Invoke();
        }
    }
}