using UnityEngine;

namespace Audio.Runtime
{
    [RequireComponent(typeof(AudioSource))]
    public class SimpleMusicPlayer : MonoBehaviour
    {
        [SerializeField] private GameAudioSettings _audioSettings;
        [SerializeField] private AudioClip _musicClip;
        [SerializeField] private float _fadeInDuration = 1.5f;

        private AudioSource _audioSource;
        private float _fadeTimer;
        private bool _isFadingIn;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = true;
            _audioSource.spatialBlend = 0f;
        }

        private void OnEnable()
        {
            if (_audioSettings != null)
            {
                _audioSettings.OnSettingsChanged += ApplyVolume;
            }

            StartMusic();
        }

        private void OnDisable()
        {
            if (_audioSettings != null)
            {
                _audioSettings.OnSettingsChanged -= ApplyVolume;
            }
        }

        private void Update()
        {
            if (!_isFadingIn) return;

            _fadeTimer += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(_fadeTimer / _fadeInDuration);
            _audioSource.volume = ComputeTargetVolume() * progress;

            if (progress >= 1f)
            {
                _isFadingIn = false;
            }
        }

        private void StartMusic()
        {
            if (_musicClip == null) return;

            _audioSource.clip = _musicClip;
            _audioSource.volume = 0f;
            _audioSource.Play();

            _fadeTimer = 0f;
            _isFadingIn = true;
        }

        private void ApplyVolume()
        {
            if (_isFadingIn) return;
            _audioSource.volume = ComputeTargetVolume();
        }

        private float ComputeTargetVolume()
        {
            return _audioSettings != null ? _audioSettings.EffectiveMusicVolume : 1f;
        }
    }
}