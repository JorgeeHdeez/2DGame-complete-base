using Items.Runtime;
using UnityEngine;

namespace Audio.Runtime
{
    [RequireComponent(typeof(LootHealth))]
    public class HealPickupSoundPlayer : MonoBehaviour
    {
        [SerializeField] private GameAudioSettings _audioSettings;
        [SerializeField] private AudioClip _pickupClip;
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;

        private LootHealth _lootHealth;

        private void Awake()
        {
            _lootHealth = GetComponent<LootHealth>();
        }

        private void OnEnable()
        {
            _lootHealth.OnHealApplied += HandleHealApplied;
        }

        private void OnDisable()
        {
            _lootHealth.OnHealApplied -= HandleHealApplied;
        }

        private void HandleHealApplied()
        {
            if (_pickupClip == null) return;
            float finalVolume = _volume * GetEffectiveVolume();
            AudioSource.PlayClipAtPoint(_pickupClip, transform.position, finalVolume);
        }

        private float GetEffectiveVolume()
        {
            return _audioSettings != null ? _audioSettings.EffectiveSfxVolume : 1f;
        }
    }
}