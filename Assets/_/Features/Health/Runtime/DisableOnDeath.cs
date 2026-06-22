using UnityEngine;

namespace Health.Runtime
{
    [RequireComponent(typeof(Health))]
    public class DisableOnDeath : MonoBehaviour
    {
        [SerializeField] private Behaviour[] _componentsToDisable;
        [SerializeField] private GameObject[] _gameObjectsToDisable;

        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable() => _health.OnDeath += HandleDeath;
       
        private void OnDisable() => _health.OnDeath -= HandleDeath;
 
        private void HandleDeath()
        {
            for (int i = 0; i < _componentsToDisable.Length; i++)
            {
                if (_componentsToDisable[i] != null)
                {
                    _componentsToDisable[i].enabled = false;
                }
            }

            for (int i = 0; i < _gameObjectsToDisable.Length; i++)
            {
                if (_gameObjectsToDisable[i] != null)
                {
                    _gameObjectsToDisable[i].SetActive(false);
                }
            }
        }
    }
}
