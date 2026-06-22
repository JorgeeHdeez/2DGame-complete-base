using Gun.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Runtime
{
    public class GunPartSlotUI : MonoBehaviour
    {
        [SerializeField] private GunPartSlot _slotType;
        [SerializeField] private TextMeshProUGUI _slotLabel;
        [SerializeField] private TextMeshProUGUI _partLabel;
        [SerializeField] private string _emptyText = "—";
        [SerializeField] private Image _iconImage;

        public GunPartSlot SlotType => _slotType;

        private void Awake()
        {
            UpdateSlotLabel();
        }

        public void DisplayPart(GunPartData part)
        {
            if (_partLabel != null)
            {
                _partLabel.text = part != null ? part.DisplayName : _emptyText;
            }

            if (_iconImage != null)
            {
                _iconImage.sprite = part != null ? part.Icon : null;
                _iconImage.enabled = part != null;
            }
        }

        private void UpdateSlotLabel()
        {
            if (_slotLabel == null) return;
            _slotLabel.text = _slotType.ToString();
        }
    }
}