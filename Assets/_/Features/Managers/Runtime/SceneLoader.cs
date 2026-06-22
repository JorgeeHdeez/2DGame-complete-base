using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers.Runtime
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string _mainMenuSceneName = "01_MainMenu";
        [SerializeField] private string _gameSceneName = "02_Game";
        [SerializeField] private string _endScreenSceneName = "03_EndScreen";

        public void LoadMainMenu()
        {
            LoadScene(_mainMenuSceneName);
        }

        public void LoadGame()
        {
            LoadScene(_gameSceneName);
        }

        public void LoadEndScreen()
        {
            LoadScene(_endScreenSceneName);
        }

        public void QuitApplication()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void LoadScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName)) return;

            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        }
    }
}