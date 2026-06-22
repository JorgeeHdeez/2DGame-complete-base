using Projectile.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Runtime
{
    public class ReloadBarUI : MonoBehaviour
    {
        [SerializeField] private Ammo _ammo;
        [SerializeField] private Image _fillImage;
        [SerializeField] private GameObject _container;

        private void OnEnable()
        {
            if (_ammo != null)
            {
                _ammo.OnReloadStarted += HandleReloadStarted;
                _ammo.OnReloadFinished += HandleReloadFinished;
            }
            HideContainer();
        }

        private void OnDisable()
        {
            if (_ammo != null)
            {
                _ammo.OnReloadStarted -= HandleReloadStarted;
                _ammo.OnReloadFinished -= HandleReloadFinished;
            }
        }

        private void Update()
        {
            if (_ammo == null || !_ammo.IsReloading) return;
            UpdateFill();
        }

        private void HandleReloadStarted()
        {
            ShowContainer();
            UpdateFill();
        }

        private void HandleReloadFinished()
        {
            HideContainer();
        }

        private void UpdateFill()
        {
            if (_fillImage == null || _ammo == null) return;
            _fillImage.fillAmount = _ammo.ReloadProgress;
        }

        private void ShowContainer()
        {
            if (_container != null) _container.SetActive(true);
        }

        private void HideContainer()
        {
            if (_container != null) _container.SetActive(false);
        }
    }
}