using Projectile.Runtime;
using TMPro;
using UnityEngine;

namespace UI.Runtime
{
    public class AmmoCountUI : MonoBehaviour
    {
        [SerializeField] private Ammo _ammo;
        [SerializeField] private TextMeshProUGUI _label;

        private void OnEnable()
        {
            if (_ammo != null)
            {
                _ammo.OnAmmoChanged += HandleAmmoChanged;
                HandleAmmoChanged(_ammo.CurrentAmmo, _ammo.MaxAmmo);
            }
        }

        private void OnDisable()
        {
            if (_ammo != null)
            {
                _ammo.OnAmmoChanged -= HandleAmmoChanged;
            }
        }

        private void HandleAmmoChanged(int current, int max)
        {
            if (_label == null) return;
            _label.text = $"{current} / {max}";
        }
    }
}