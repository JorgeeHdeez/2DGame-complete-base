using UnityEngine;

namespace Audio.Runtime
{
    public class AudioVolumeApplier : MonoBehaviour
    {
        [SerializeField] private GameAudioSettings gameAudioSettings;

        private void OnEnable()
        {
            if (gameAudioSettings != null)
            {
                gameAudioSettings.OnSettingsChanged += ApplyVolume;
                ApplyVolume();
            }
        }

        private void OnDisable()
        {
            if (gameAudioSettings != null)
            {
                gameAudioSettings.OnSettingsChanged -= ApplyVolume;
            }
        }

        private void ApplyVolume()
        {
            if (gameAudioSettings == null) return;
            AudioListener.volume = gameAudioSettings.MasterVolume;
        }
    }
}