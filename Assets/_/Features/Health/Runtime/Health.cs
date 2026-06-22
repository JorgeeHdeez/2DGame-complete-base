using UnityEngine;
using System;

namespace Health.Runtime
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _currentHealth = 100;

        public event Action<int, int> OnHealthChanged;
        public event Action<int> OnDamaged;
        public event Action<int> OnHealed;
        public event Action OnDeath;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public bool IsDead => _currentHealth <= 0;

        private void Awake()
        {
            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0 || IsDead) return;

            _currentHealth = Mathf.Max(0, _currentHealth - amount);
            OnDamaged?.Invoke(amount);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void Heal(int amount)
        {
            if (amount <= 0 || IsDead) return;

            _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
            OnHealed?.Invoke(amount);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        public void SetMaxHealth(int newMax)
        {
            _maxHealth = Mathf.Max(1, newMax);
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
        
        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }
}
