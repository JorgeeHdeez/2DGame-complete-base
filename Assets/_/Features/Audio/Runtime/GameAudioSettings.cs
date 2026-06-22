using System;
using UnityEngine;

namespace Audio.Runtime
{
    [CreateAssetMenu(fileName = "GameAudioSettings", menuName = "Audio/Game Audio Settings")]
    public class GameAudioSettings : ScriptableObject
    {
        [SerializeField, Range(0f, 1f)] private float _masterVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float _sfxVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float _musicVolume = 0.5f;

        [Header("Pause Behavior")]
        [SerializeField, Range(0f, 1f)] private float _pausedMusicVolumeRatio = 0.3f;

        public event Action OnSettingsChanged;

        public float MasterVolume => _masterVolume;
        public float SfxVolume => _sfxVolume;
        public float MusicVolume => _musicVolume;
        public float EffectiveSfxVolume => _masterVolume * _sfxVolume;
        public float EffectiveMusicVolume => _masterVolume * _musicVolume;
        public float PausedMusicVolumeRatio => _pausedMusicVolumeRatio;

        public void SetMasterVolume(float value)
        {
            _masterVolume = Mathf.Clamp01(value);
            OnSettingsChanged?.Invoke();
        }

        public void SetSfxVolume(float value)
        {
            _sfxVolume = Mathf.Clamp01(value);
            OnSettingsChanged?.Invoke();
        }

        public void SetMusicVolume(float value)
        {
            _musicVolume = Mathf.Clamp01(value);
            OnSettingsChanged?.Invoke();
        }
    }
}