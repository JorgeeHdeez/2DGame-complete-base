using Common.Runtime;
using Health.Runtime;
using UnityEngine;

namespace Audio.Runtime
{
    [RequireComponent(typeof(Health.Runtime.Health))]
    public class HealthSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioPoolReference _audioPoolRef;
        [SerializeField] private GameAudioSettings _audioSettings;
        [SerializeField] private AudioClip _damageClip;
        [SerializeField] private AudioClip _deathClip;
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;

        private Health.Runtime.Health _health;

        private void Awake()
        {
            _health = GetComponent<Health.Runtime.Health>();
        }

        private void OnEnable()
        {
            _health.OnDamaged += HandleDamaged;
            _health.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            _health.OnDamaged -= HandleDamaged;
            _health.OnDeath -= HandleDeath;
        }

        private void HandleDamaged(int amount)
        {
            PlaySound(_damageClip);
        }

        private void HandleDeath()
        {
            PlaySound(_deathClip);
        }

        private void PlaySound(AudioClip clip)
        {
            if (clip == null) return;
            if (_audioPoolRef == null || _audioPoolRef.Pool == null) return;

            float finalVolume = _volume * GetEffectiveVolume();
            _audioPoolRef.Pool.PlayClip(clip, transform.position, finalVolume);
        }

        private float GetEffectiveVolume()
        {
            return _audioSettings != null ? _audioSettings.EffectiveSfxVolume : 1f;
        }
    }
}