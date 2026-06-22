using UnityEngine;

namespace Audio.Runtime
{
    [RequireComponent(typeof(Interactable.Runtime.Interactable))]
    public class PickupSoundPlayer : MonoBehaviour
    {
        [SerializeField] private GameAudioSettings _audioSettings;
        [SerializeField] private AudioClip _pickupClip;
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;

        private Interactable.Runtime.Interactable _interactable;

        private void Awake()
        {
            _interactable = GetComponent<Interactable.Runtime.Interactable>();
        }

        private void OnEnable()
        {
            _interactable.OnInteract += HandleInteract;
        }

        private void OnDisable()
        {
            _interactable.OnInteract -= HandleInteract;
        }

        private void HandleInteract(GameObject interactor)
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