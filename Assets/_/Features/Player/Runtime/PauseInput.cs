using Managers.Runtime;
using UnityEngine;

namespace Player.Runtime
{
    public class PauseInput : MonoBehaviour
    {
        [SerializeField] private PauseManager _pauseManager;
        [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;
        [SerializeField] private GameManager _gameManager;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (_pauseManager == null) return;
            if (_gameManager != null && _gameManager.IsGameOver) return;

            if (Input.GetKeyDown(_pauseKey))
            {
                _pauseManager.TogglePause();
            }
        }
    }
}