using System;
using UnityEngine;

namespace Gun.Runtime
{
    public class Gun : MonoBehaviour
    {
        [Header("Base Stats")]
        [SerializeField] private int _baseDamage = 10;
        [SerializeField] private float _baseFireRate = 5f;
        [SerializeField] private int _baseMagazineSize = 10;
        [SerializeField] private float _baseReloadDuration = 2f;
        [SerializeField] private float _baseSpread = 5f;

        [Header("Initial Parts (Optional)")]
        [SerializeField] private GunPartData _trigger;
        [SerializeField] private GunPartData _magazine;
        [SerializeField] private GunPartData _grip;

        public event Action<GunStats> OnStatsChanged;

        private GunStats _currentStats;

        public GunStats CurrentStats => _currentStats;

        private void Awake()
        {
            RecalculateStats();
        }

        private void Start()
        {
            OnStatsChanged?.Invoke(_currentStats);
        }

        public bool TryEquipPart(GunPartData part)
        {
            if (part == null) return false;

            switch (part.Slot)
            {
                case GunPartSlot.Trigger:
                    _trigger = part;
                    break;
                case GunPartSlot.Magazine:
                    _magazine = part;
                    break;
                case GunPartSlot.Grip:
                    _grip = part;
                    break;
                default:
                    return false;
            }

            RecalculateStats();
            OnStatsChanged?.Invoke(_currentStats);
            return true;
        }

        public GunPartData GetPart(GunPartSlot slot)
        {
            switch (slot)
            {
                case GunPartSlot.Trigger: return _trigger;
                case GunPartSlot.Magazine: return _magazine;
                case GunPartSlot.Grip: return _grip;
                default: return null;
            }
        }

        private void RecalculateStats()
        {
            int damage = _baseDamage;
            float fireRate = _baseFireRate;
            int magazineSize = _baseMagazineSize;
            float reloadDuration = _baseReloadDuration;
            float spread = _baseSpread;

            ApplyPart(_trigger, ref damage, ref fireRate, ref magazineSize, ref reloadDuration, ref spread);
            ApplyPart(_magazine, ref damage, ref fireRate, ref magazineSize, ref reloadDuration, ref spread);
            ApplyPart(_grip, ref damage, ref fireRate, ref magazineSize, ref reloadDuration, ref spread);

            _currentStats = new GunStats
            {
                Damage = Mathf.Max(0, damage),
                FireRate = Mathf.Max(0.1f, fireRate),
                MagazineSize = Mathf.Max(1, magazineSize),
                ReloadDuration = Mathf.Max(0.1f, reloadDuration),
                Spread = Mathf.Max(0f, spread)
            };
        }

        private void ApplyPart(GunPartData part, ref int damage, ref float fireRate, ref int magazineSize, ref float reloadDuration, ref float spread)
        {
            if (part == null) return;

            damage += part.DamageBonus;
            fireRate *= part.FireRateMultiplier;
            magazineSize += part.MagazineSizeBonus;
            reloadDuration *= part.ReloadSpeedMultiplier;
            spread += part.SpreadBonus;
        }
    }
}