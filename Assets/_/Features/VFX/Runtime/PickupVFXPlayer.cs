using Interactable.Runtime;
using UnityEngine;

namespace VFX.Runtime
{
    [RequireComponent(typeof(Interactable.Runtime.Interactable))]
    public class PickupVFXPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _pickupVFXPrefab;

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
            if (_pickupVFXPrefab == null) return;
            Instantiate(_pickupVFXPrefab, transform.position, Quaternion.identity);
        }
    }
}