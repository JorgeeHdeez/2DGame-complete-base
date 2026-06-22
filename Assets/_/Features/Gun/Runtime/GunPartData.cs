using UnityEngine;

namespace Gun.Runtime
{
    [CreateAssetMenu(fileName = "GunPart", menuName = "Gun/Gun Part Data")]
    public class GunPartData : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] private GunPartSlot _slot = GunPartSlot.None;
        [SerializeField] private string _displayName = "Unnamed Part";
        [SerializeField] private Sprite _icon; public Sprite Icon => _icon;

        [Header("Damage (Additive)")]
        [SerializeField] private int _damageBonus = 0;

        [Header("Fire Rate (Multiplicative, 1.0 = no change)")]
        [SerializeField] private float _fireRateMultiplier = 1f;

        [Header("Magazine (Additive)")]
        [SerializeField] private int _magazineSizeBonus = 0;

        [Header("Reload Speed (Multiplicative, 1.0 = no change, <1 = faster)")]
        [SerializeField] private float _reloadSpeedMultiplier = 1f;

        [Header("Accuracy (Additive, in degrees of spread, negative = better)")]
        [SerializeField] private float _spreadBonus = 0f;
        
        public GunPartSlot Slot => _slot;
        public string DisplayName => _displayName;
        public int DamageBonus => _damageBonus;
        public float FireRateMultiplier => _fireRateMultiplier;
        public int MagazineSizeBonus => _magazineSizeBonus;
        public float ReloadSpeedMultiplier => _reloadSpeedMultiplier;
        public float SpreadBonus => _spreadBonus;
    }
}