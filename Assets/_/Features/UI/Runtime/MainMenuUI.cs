using Managers.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Runtime
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _quitButton;

        private void OnEnable()
        {
            if (_startButton != null)
            {
                _startButton.onClick.AddListener(HandleStartClicked);
            }

            if (_quitButton != null)
            {
                _quitButton.onClick.AddListener(HandleQuitClicked);
            }
        }

        private void OnDisable()
        {
            if (_startButton != null)
            {
                _startButton.onClick.RemoveListener(HandleStartClicked);
            }

            if (_quitButton != null)
            {
                _quitButton.onClick.RemoveListener(HandleQuitClicked);
            }
        }

        private void HandleStartClicked()
        {
            if (_sceneLoader != null)
            {
                _sceneLoader.LoadGame();
            }
        }

        private void HandleQuitClicked()
        {
            if (_sceneLoader != null)
            {
                _sceneLoader.QuitApplication();
            }
        }
    }
}