using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Runtime
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "Items/Loot Table")]
    public class LootTable : ScriptableObject
    {
        [Serializable]
        public struct LootEntry
        {
            public GameObject Prefab;
            public float Weight;
        }

        [SerializeField] private List<LootEntry> _entries = new List<LootEntry>();
        [SerializeField, Range(0f, 1f)] private float _dropChance = 0.5f;

        public bool TryRoll(out GameObject prefab)
        {
            prefab = null;

            if (_entries == null || _entries.Count == 0) return false;
            if (UnityEngine.Random.value > _dropChance) return false;

            float totalWeight = ComputeTotalWeight();
            if (totalWeight <= 0f) return false;

            float roll = UnityEngine.Random.Range(0f, totalWeight);
            float cumulative = 0f;

            for (int i = 0; i < _entries.Count; i++)
            {
                cumulative += Mathf.Max(0f, _entries[i].Weight);
                if (roll <= cumulative)
                {
                    prefab = _entries[i].Prefab;
                    return prefab != null;
                }
            }

            return false;
        }

        private float ComputeTotalWeight()
        {
            float total = 0f;
            for (int i = 0; i < _entries.Count; i++)
            {
                total += Mathf.Max(0f, _entries[i].Weight);
            }
            return total;
        }
    }
}