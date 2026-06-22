using Managers.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Runtime
{
    public class EndScreenUI : MonoBehaviour
    {
        [SerializeField] private GameRunData _gameRunData;
        [SerializeField] private SceneLoader _sceneLoader;

        [Header("Display")]
        [SerializeField] private TextMeshProUGUI _killCountLabel;
        [SerializeField] private TextMeshProUGUI _timeLabel;
        [SerializeField] private TextMeshProUGUI _coinCountLabel;
        [SerializeField] private string _killCountFormat = "Kills: {0}";
        [SerializeField] private string _coinCountFormat = "Coins: {0}";

        [Header("Buttons")]
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _retryButton;

        private void OnEnable()
        {
            DisplayStats();

            if (_mainMenuButton != null)
            {
                _mainMenuButton.onClick.AddListener(HandleMainMenuClicked);
            }

            if (_retryButton != null)
            {
                _retryButton.onClick.AddListener(HandleRetryClicked);
            }
        }

        private void OnDisable()
        {
            if (_mainMenuButton != null)
            {
                _mainMenuButton.onClick.RemoveListener(HandleMainMenuClicked);
            }

            if (_retryButton != null)
            {
                _retryButton.onClick.RemoveListener(HandleRetryClicked);
            }
        }

        private void DisplayStats()
        {
            if (_gameRunData == null) return;

            if (_killCountLabel != null)
            {
                _killCountLabel.text = string.Format(_killCountFormat, _gameRunData.KillCount);
            }

            if (_timeLabel != null)
            {
                _timeLabel.text = FormatTime(_gameRunData.ElapsedSeconds);
            }

            if (_coinCountLabel != null)
            {
                _coinCountLabel.text = string.Format(_coinCountFormat, _gameRunData.CoinCount);
            }
        }
        private string FormatTime(float elapsedSeconds)
        {
            int minutes = Mathf.FloorToInt(elapsedSeconds / 60f);
            int seconds = Mathf.FloorToInt(elapsedSeconds % 60f);
            return $"{minutes:00}:{seconds:00}";
        }

        private void HandleMainMenuClicked()
        {
            if (_sceneLoader != null)
            {
                _sceneLoader.LoadMainMenu();
            }
        }

        private void HandleRetryClicked()
        {
            if (_sceneLoader != null)
            {
                _sceneLoader.LoadGame();
            }
        }
    }
}