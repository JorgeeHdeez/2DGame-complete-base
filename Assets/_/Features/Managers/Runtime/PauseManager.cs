using System;
using UnityEngine;

namespace Managers.Runtime
{
    public class PauseManager : MonoBehaviour
    {
        public event Action OnPaused;
        public event Action OnResumed;

        private bool _isPaused;

        public bool IsPaused => _isPaused;

        public void Pause()
        {
            if (_isPaused) return;
            _isPaused = true;
            Time.timeScale = 0f;
            OnPaused?.Invoke();
        }

        public void Resume()
        {
            if (!_isPaused) return;
            _isPaused = false;
            Time.timeScale = 1f;
            OnResumed?.Invoke();
        }

        public void TogglePause()
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        private void OnDisable()
        {
            if (_isPaused)
            {
                Time.timeScale = 1f;
            }
        }
    }
}