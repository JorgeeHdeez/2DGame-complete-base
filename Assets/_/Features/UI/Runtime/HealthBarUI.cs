using Health.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Runtime
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Health.Runtime.Health _health;
        [SerializeField] private Image _fillImage;

        private void OnEnable()
        {
            if (_health != null)
            {
                _health.OnHealthChanged += HandleHealthChanged;
                HandleHealthChanged(_health.CurrentHealth, _health.MaxHealth);
            }
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.OnHealthChanged -= HandleHealthChanged;
            }
        }

        private void HandleHealthChanged(int current, int max)
        {
            if (_fillImage == null) return;
            if (max <= 0) return;

            _fillImage.fillAmount = (float)current / max;
        }
    }
}