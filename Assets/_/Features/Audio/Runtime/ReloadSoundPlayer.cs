using Common.Runtime;
using Projectile.Runtime;
using UnityEngine;

namespace Audio.Runtime
{
    public class ReloadSoundPlayer : MonoBehaviour
    {
        [SerializeField] private Ammo _ammo;
        [SerializeField] private AudioPoolReference _audioPoolRef;
        [SerializeField] private GameAudioSettings _audioSettings;
        [SerializeField] private AudioClip _reloadClip;
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;

        private void OnEnable()
        {
            if (_ammo != null) _ammo.OnReloadStarted += HandleReloadStarted;
        }

        private void OnDisable()
        {
            if (_ammo != null) _ammo.OnReloadStarted -= HandleReloadStarted;
        }

        private void HandleReloadStarted()
        {
            if (_reloadClip == null) return;
            if (_audioPoolRef == null || _audioPoolRef.Pool == null) return;

            float finalVolume = _volume * GetEffectiveVolume();
            _audioPoolRef.Pool.PlayClip(_reloadClip, transform.position, finalVolume);
        }

        private float GetEffectiveVolume()
        {
            return _audioSettings != null ? _audioSettings.EffectiveSfxVolume : 1f;
        }
    }
}