using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable.Runtime
{
    public class Inventory : MonoBehaviour
    {
        private readonly Dictionary<ItemType, int> _items = new Dictionary<ItemType, int>();

        public event Action<ItemType, int> OnItemAdded;
        public event Action<ItemType, int> OnItemChanged;

        public int GetCount(ItemType type)
        {
            return _items.TryGetValue(type, out int count) ? count : 0;
        }

        public bool HasItem(ItemType type)
        {
            return GetCount(type) > 0;
        }

        public void AddItem(ItemType type, int amount)
        {
            if (amount <= 0) return;

            if (_items.ContainsKey(type))
            {
                _items[type] += amount;
            }
            else
            {
                _items[type] = amount;
            }
            
            Debug.Log($"[Inventory] Added {amount}x {type} → Total: {_items[type]}");

            OnItemAdded?.Invoke(type, amount);
            OnItemChanged?.Invoke(type, _items[type]);
        }

        public bool RemoveItem(ItemType type, int amount)
        {
            if (amount <= 0) return false;
            if (!_items.ContainsKey(type) || _items[type] < amount) return false;

            _items[type] -= amount;
            if (_items[type] <= 0)
            {
                _items.Remove(type);
            }

            int newCount = GetCount(type);
            OnItemChanged?.Invoke(type, newCount);
            return true;
        }
    }
}