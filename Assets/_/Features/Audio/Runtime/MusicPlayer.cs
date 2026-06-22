using Managers.Runtime;
using UnityEngine;

namespace Audio.Runtime
{
    public class MusicPlayer : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameAudioSettings _gameAudioSettings;
        [SerializeField] private IntensityTracker _intensityTracker;
        [SerializeField] private PauseManager _pauseManager;

        [Header("Music Clips")]
        [SerializeField] private AudioClip _calmClip;
        [SerializeField] private AudioClip _combatClip;
        [SerializeField] private AudioClip _intenseClip;

        [Header("Transitions")]
        [SerializeField] private float _fadeInDuration = 1.5f;
        [SerializeField] private float _crossfadeDuration = 1.5f;

        private AudioSource _sourceA;
        private AudioSource _sourceB;
        private AudioSource _activeSource;
        private AudioSource _fadingOutSource;

        private MusicIntensity _currentIntensity;
        private float _crossfadeTimer;
        private bool _isCrossfading;
        private bool _isInitialFadeIn;

        private void Awake()
        {
            _sourceA = CreateSource("Source_A");
            _sourceB = CreateSource("Source_B");
            _activeSource = _sourceA;
        }

        private AudioSource CreateSource(string sourceName)
        {
            GameObject go = new GameObject(sourceName);
            go.transform.SetParent(transform);

            AudioSource source = go.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = true;
            source.spatialBlend = 0f;
            source.volume = 0f;
            return source;
        }

        private void OnEnable()
        {
            if (_gameAudioSettings != null)
            {
                _gameAudioSettings.OnSettingsChanged += ApplyVolume;
            }

            if (_intensityTracker != null)
            {
                _intensityTracker.OnIntensityChanged += HandleIntensityChanged;
                _currentIntensity = _intensityTracker.CurrentIntensity;
            }

            StartInitialMusic();
        }

        private void OnDisable()
        {
            if (_gameAudioSettings != null) _gameAudioSettings.OnSettingsChanged -= ApplyVolume;
            if (_intensityTracker != null) _intensityTracker.OnIntensityChanged -= HandleIntensityChanged;
        }

        private void Update()
        {
            HandleFading();
        }

        private void StartInitialMusic()
        {
            AudioClip clip = GetClipForIntensity(_currentIntensity);
            if (clip == null) return;

            _activeSource.clip = clip;
            _activeSource.volume = 0f;
            _activeSource.Play();

            _crossfadeTimer = 0f;
            _isInitialFadeIn = true;
            _isCrossfading = false;
        }

        private void HandleIntensityChanged(MusicIntensity newIntensity)
        {
            if (newIntensity == _currentIntensity) return;

            AudioClip newClip = GetClipForIntensity(newIntensity);
            _currentIntensity = newIntensity;

            if (newClip == null) return;

            StartCrossfade(newClip);
        }

        private void StartCrossfade(AudioClip newClip)
        {
            _fadingOutSource = _activeSource;
            _activeSource = (_activeSource == _sourceA) ? _sourceB : _sourceA;

            _activeSource.clip = newClip;
            _activeSource.volume = 0f;
            _activeSource.time = 0f;
            _activeSource.Play();

            _crossfadeTimer = 0f;
            _isCrossfading = true;
            _isInitialFadeIn = false;
        }

        private void HandleFading()
        {
            float targetVolume = ComputeTargetVolume();

            if (_isInitialFadeIn)
            {
                _crossfadeTimer += Time.unscaledDeltaTime;
                float progress = Mathf.Clamp01(_crossfadeTimer / _fadeInDuration);
                _activeSource.volume = targetVolume * progress;

                if (progress >= 1f)
                {
                    _isInitialFadeIn = false;
                }
            }
            else if (_isCrossfading)
            {
                _crossfadeTimer += Time.unscaledDeltaTime;
                float progress = Mathf.Clamp01(_crossfadeTimer / _crossfadeDuration);

                _activeSource.volume = targetVolume * progress;
                if (_fadingOutSource != null)
                {
                    _fadingOutSource.volume = targetVolume * (1f - progress);
                }

                if (progress >= 1f)
                {
                    _isCrossfading = false;
                    if (_fadingOutSource != null)
                    {
                        _fadingOutSource.Stop();
                        _fadingOutSource = null;
                    }
                }
            }
            else
            {
                _activeSource.volume = targetVolume;
            }
        }

        private void ApplyVolume()
        {
            // Le HandleFading reapplique chaque frame, donc rien à faire ici.
            // On garde la méthode pour les autres systèmes audio qui s'abonnent.
        }

        private float ComputeTargetVolume()
        {
            float baseVolume = _gameAudioSettings != null ? _gameAudioSettings.EffectiveMusicVolume : 1f;

            if (IsPaused())
            {
                float ratio = _gameAudioSettings != null ? _gameAudioSettings.PausedMusicVolumeRatio : 0.3f;
                return baseVolume * ratio;
            }

            return baseVolume;
        }

        private bool IsPaused()
        {
            return _pauseManager != null && _pauseManager.IsPaused;
        }

        private AudioClip GetClipForIntensity(MusicIntensity intensity)
        {
            switch (intensity)
            {
                case MusicIntensity.Calm: return _calmClip;
                case MusicIntensity.Combat: return _combatClip;
                case MusicIntensity.Intense: return _intenseClip;
                default: return _calmClip;
            }
        }
    }
}