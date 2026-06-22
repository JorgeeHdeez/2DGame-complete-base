using Gun.Runtime;
using Interactable.Runtime;
using UnityEngine;

namespace Items.Runtime
{
    [RequireComponent(typeof(Interactable.Runtime.Interactable))]
    public class LootGunPart : MonoBehaviour
    {
        [SerializeField] private GunPartData _partData;

        private Interactable.Runtime.Interactable _interactable;

        private void Awake()
        {
            _interactable = GetComponent<Interactable.Runtime.Interactable>();
        }

        private void OnEnable()
        {
            _interactable.OnInteract += HandlePickup;
        }

        private void OnDisable()
        {
            _interactable.OnInteract -= HandlePickup;
        }

        private void HandlePickup(GameObject interactor)
        {
            if (_partData == null) return;

            Gun.Runtime.Gun gun = interactor.GetComponent<Gun.Runtime.Gun>();
            if (gun == null) return;

            bool success = gun.TryEquipPart(_partData);
            if (success)
            {
                Destroy(gameObject);
            }
        }

        public void SetPartData(GunPartData data)
        {
            _partData = data;
        }
    }
}