using System;
using UnityEngine;

namespace Managers.Runtime
{
    public class GameTimer : MonoBehaviour, IUpdatable
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private UpdateManager _updateManager;

        public event Action<float> OnTimeChanged;

        private float _elapsedSeconds;
        private bool _isRunning = true;

        public float ElapsedSeconds => _elapsedSeconds;
        public bool IsRunning => _isRunning;

        private void OnEnable()
        {
            if (_updateManager != null)
            {
                _updateManager.Register(this);
            }

            if (_gameManager != null)
            {
                _gameManager.OnGameOver += HandleGameOver;
            }
        }

        private void OnDisable()
        {
            if (_updateManager != null)
            {
                _updateManager.Unregister(this);
            }

            if (_gameManager != null)
            {
                _gameManager.OnGameOver -= HandleGameOver;
            }
        }

        public void OnTick(float deltaTime)
        {
            if (!_isRunning) return;

            _elapsedSeconds += deltaTime;
            OnTimeChanged?.Invoke(_elapsedSeconds);
        }

        public void StartTimer()
        {
            _isRunning = true;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        public void ResetTimer()
        {
            _elapsedSeconds = 0f;
            OnTimeChanged?.Invoke(_elapsedSeconds);
        }

        private void HandleGameOver()
        {
            StopTimer();
        }
    }
}