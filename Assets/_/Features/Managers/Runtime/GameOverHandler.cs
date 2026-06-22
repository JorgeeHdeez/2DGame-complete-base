using UnityEngine;

namespace Managers.Runtime
{
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private float _delayBeforeLoad = 3f;

        private float _timer;
        private bool _isCountingDown;

        private void OnEnable()
        {
            if (_gameManager != null)
            {
                _gameManager.OnGameOver += HandleGameOver;
            }
        }

        private void OnDisable()
        {
            if (_gameManager != null)
            {
                _gameManager.OnGameOver -= HandleGameOver;
            }
        }

        private void Update()
        {
            if (!_isCountingDown) return;

            _timer += Time.unscaledDeltaTime;
            if (_timer >= _delayBeforeLoad)
            {
                _isCountingDown = false;
                LoadDestination();
            }
        }

        private void HandleGameOver()
        {
            _timer = 0f;
            _isCountingDown = true;
        }

        private void LoadDestination()
        {
            if (_sceneLoader != null)
            {
                _sceneLoader.LoadEndScreen();
            }
        }
    }
}