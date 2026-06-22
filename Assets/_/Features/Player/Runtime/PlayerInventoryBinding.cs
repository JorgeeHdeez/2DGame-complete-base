using Interactable.Runtime;
using Managers.Runtime;
using UnityEngine;

namespace Player.Runtime
{
    public class PlayerInventoryBinding : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameRunData _gameRunData;
        [SerializeField] private ItemType _itemTypeToTrack = ItemType.Coin;

        private void OnEnable()
        {
            if (_gameManager != null)
            {
                _gameManager.OnGameOver += HandleGameOver;
            }
        }

        private void OnDisable()
        {
            if (_gameManager != null)
            {
                _gameManager.OnGameOver -= HandleGameOver;
            }
        }

        private void HandleGameOver()
        {
            if (_inventory == null || _gameRunData == null) return;

            int count = _inventory.GetCount(_itemTypeToTrack);
            _gameRunData.RecordCoins(count);
        }
    }
}