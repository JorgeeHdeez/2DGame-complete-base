using Gun.Runtime;
using UnityEngine;

namespace UI.Runtime
{
    public class GunPartsUI : MonoBehaviour
    {
        [SerializeField] private Gun.Runtime.Gun _gun;
        [SerializeField] private GunPartSlotUI[] _slotUIs;

        private void OnEnable()
        {
            if (_gun != null)
            {
                _gun.OnStatsChanged += HandleStatsChanged;
                RefreshAllSlots();
            }
        }

        private void OnDisable()
        {
            if (_gun != null)
            {
                _gun.OnStatsChanged -= HandleStatsChanged;
            }
        }

        private void HandleStatsChanged(GunStats stats)
        {
            RefreshAllSlots();
        }

        private void RefreshAllSlots()
        {
            if (_slotUIs == null || _gun == null) return;

            for (int i = 0; i < _slotUIs.Length; i++)
            {
                GunPartSlotUI slotUI = _slotUIs[i];
                if (slotUI == null) continue;

                GunPartData part = _gun.GetPart(slotUI.SlotType);
                slotUI.DisplayPart(part);
            }
        }
    }
}