using Managers.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Runtime
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] private PauseManager _pauseManager;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private GameObject _container;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _quitButton;

        private void OnEnable()
        {
            if (_pauseManager != null)
            {
                _pauseManager.OnPaused += HandlePaused;
                _pauseManager.OnResumed += HandleResumed;
            }

            if (_resumeButton != null)
            {
                _resumeButton.onClick.AddListener(HandleResumeClicked);
            }

            if (_quitButton != null)
            {
                _quitButton.onClick.AddListener(HandleQuitClicked);
            }

            HideContainer();
        }

        private void OnDisable()
        {
            if (_pauseManager != null)
            {
                _pauseManager.OnPaused -= HandlePaused;
                _pauseManager.OnResumed -= HandleResumed;
            }

            if (_resumeButton != null)
            {
                _resumeButton.onClick.RemoveListener(HandleResumeClicked);
            }

            if (_quitButton != null)
            {
                _quitButton.onClick.RemoveListener(HandleQuitClicked);
            }
        }

        private void HandlePaused()
        {
            ShowContainer();
        }

        private void HandleResumed()
        {
            HideContainer();
        }

        private void HandleResumeClicked()
        {
            if (_pauseManager != null)
            {
                _pauseManager.Resume();
            }
        }

        private void HandleQuitClicked()
        {
            if (_pauseManager != null)
            {
                _pauseManager.Resume();
            }

            if (_sceneLoader != null)
            {
                _sceneLoader.LoadMainMenu();
            }
        }

        private void ShowContainer()
        {
            if (_container != null) _container.SetActive(true);
        }

        private void HideContainer()
        {
            if (_container != null) _container.SetActive(false);
        }
    }
}