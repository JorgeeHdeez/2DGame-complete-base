using Health.Runtime;
using UnityEngine;

namespace Managers.Runtime
{
    [RequireComponent(typeof(Health.Runtime.Health))]
    public class PlayerDeathBinding : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        private global::Health.Runtime.Health _health;

        private void Awake()
        {
            _health = GetComponent<global::Health.Runtime.Health>();
        }

        private void OnEnable() => _health.OnDeath += HandleDeath;
     
        private void OnDisable() => _health.OnDeath -= HandleDeath;
    
        private void HandleDeath()
        {
            if (_gameManager != null)
            {
                _gameManager.TriggerGameOver();
            }
        }
    }
}