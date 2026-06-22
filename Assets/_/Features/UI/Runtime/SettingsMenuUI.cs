using Audio.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Runtime
{
    public class SettingsMenuUI : MonoBehaviour
    {
        [SerializeField] private GameAudioSettings _gameAudioSettings;
        [SerializeField] private GameObject _container;
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            if (_masterVolumeSlider != null && _gameAudioSettings != null)
            {
                _masterVolumeSlider.value = _gameAudioSettings.MasterVolume;
                _masterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeChanged);
            }

            if (_sfxVolumeSlider != null && _gameAudioSettings != null)
            {
                _sfxVolumeSlider.value = _gameAudioSettings.SfxVolume;
                _sfxVolumeSlider.onValueChanged.AddListener(HandleSfxVolumeChanged);
            }

            if (_openButton != null) _openButton.onClick.AddListener(HandleOpenClicked);
            if (_closeButton != null) _closeButton.onClick.AddListener(HandleCloseClicked);

            HideContainer();
        }

        private void OnDisable()
        {
            if (_masterVolumeSlider != null) _masterVolumeSlider.onValueChanged.RemoveListener(HandleMasterVolumeChanged);
            if (_sfxVolumeSlider != null) _sfxVolumeSlider.onValueChanged.RemoveListener(HandleSfxVolumeChanged);
            if (_openButton != null) _openButton.onClick.RemoveListener(HandleOpenClicked);
            if (_closeButton != null) _closeButton.onClick.RemoveListener(HandleCloseClicked);
        }

        private void HandleMasterVolumeChanged(float value)
        {
            if (_gameAudioSettings != null) _gameAudioSettings.SetMasterVolume(value);
        }

        private void HandleSfxVolumeChanged(float value)
        {
            if (_gameAudioSettings != null) _gameAudioSettings.SetSfxVolume(value);
        }

        private void HandleOpenClicked()
        {
            ShowContainer();
        }

        private void HandleCloseClicked()
        {
            HideContainer();
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