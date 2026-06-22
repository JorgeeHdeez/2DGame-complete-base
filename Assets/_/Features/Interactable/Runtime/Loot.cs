using UnityEngine;

namespace Interactable.Runtime
{
    [RequireComponent(typeof(Interactable))]
    public class Loot : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType = ItemType.Coin;
        [SerializeField] private int _amount = 1;

        private Interactable _interactable;

        private void Awake()
        {
            _interactable = GetComponent<Interactable>();
        }

        private void OnEnable() => _interactable.OnInteract += HandlePickup;
     
        private void OnDisable() => _interactable.OnInteract -= HandlePickup;
     
        private void HandlePickup(GameObject interactor)
        {
            Inventory inventory = interactor.GetComponent<Inventory>();
            if (inventory == null) return;

            inventory.AddItem(_itemType, _amount);
            Destroy(gameObject);
        }
    }
}