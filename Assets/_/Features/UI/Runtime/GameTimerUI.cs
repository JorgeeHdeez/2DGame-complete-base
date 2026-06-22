using Managers.Runtime;
using TMPro;
using UnityEngine;

namespace UI.Runtime
{
    public class GameTimerUI : MonoBehaviour
    {
        [SerializeField] private GameTimer _gameTimer;
        [SerializeField] private TextMeshProUGUI _label;

        private void OnEnable()
        {
            if (_gameTimer != null)
            {
                _gameTimer.OnTimeChanged += HandleTimeChanged;
                HandleTimeChanged(_gameTimer.ElapsedSeconds);
            }
        }

        private void OnDisable()
        {
            if (_gameTimer != null)
            {
                _gameTimer.OnTimeChanged -= HandleTimeChanged;
            }
        }

        private void HandleTimeChanged(float elapsedSeconds)
        {
            if (_label == null) return;

            int minutes = Mathf.FloorToInt(elapsedSeconds / 60f);
            int seconds = Mathf.FloorToInt(elapsedSeconds % 60f);

            _label.text = $"{minutes:00}:{seconds:00}";
        }
    }
}