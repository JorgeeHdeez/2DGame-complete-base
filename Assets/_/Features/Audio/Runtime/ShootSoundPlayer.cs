using Common.Runtime;
using Projectile.Runtime;
using UnityEngine;

namespace Audio.Runtime
{
    public class ShootSoundPlayer : MonoBehaviour
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] private AudioPoolReference _audioPoolRef;
        [SerializeField] private GameAudioSettings _audioSettings;
        [SerializeField] private AudioClip _shootClip;
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;

        private void OnEnable()
        {
            if (_shooter != null) _shooter.OnShoot += HandleShoot;
        }

        private void OnDisable()
        {
            if (_shooter != null) _shooter.OnShoot -= HandleShoot;
        }

        private void HandleShoot()
        {
            if (_shootClip == null) return;
            if (_audioPoolRef == null || _audioPoolRef.Pool == null) return;

            float finalVolume = _volume * GetEffectiveVolume();
            _audioPoolRef.Pool.PlayClip(_shootClip, transform.position, finalVolume);
        }

        private float GetEffectiveVolume()
        {
            return _audioSettings != null ? _audioSettings.EffectiveSfxVolume : 1f;
        }
    }
}