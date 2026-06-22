using System;
using UnityEngine;

namespace Managers.Runtime
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameRunData _gameRunData;
        [SerializeField] private GameTimer _gameTimer;

        private int _killCount;
        private bool _isGameOver;

        public event Action<int> OnKillCountChanged;
        public event Action OnGameOver;

        public int KillCount => _killCount;
        public bool IsGameOver => _isGameOver;

        private void Awake()
        {
            if (_gameRunData != null)
            {
                _gameRunData.Reset();
            }
        }

        public void RegisterKill()
        {
            if (_isGameOver) return;
            _killCount++;
            OnKillCountChanged?.Invoke(_killCount);
        }

        public void TriggerGameOver()
        {
            if (_isGameOver) return;
            _isGameOver = true;

            RecordRunData();

            OnGameOver?.Invoke();
        }

        public void Reset()
        {
            _killCount = 0;
            _isGameOver = false;
            OnKillCountChanged?.Invoke(_killCount);
        }

        private void RecordRunData()
        {
            if (_gameRunData == null) return;

            float elapsed = _gameTimer != null ? _gameTimer.ElapsedSeconds : 0f;
            _gameRunData.Record(_killCount, elapsed);
        }
    }
}