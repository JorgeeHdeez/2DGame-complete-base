using Managers.Runtime;
using TMPro;
using UnityEngine;

namespace UI.Runtime
{
    public class KillCountUI : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private string _format = "Kills: {0}";

        private void OnEnable()
        {
            if (_gameManager != null)
            {
                _gameManager.OnKillCountChanged += HandleKillCountChanged;
                HandleKillCountChanged(_gameManager.KillCount);
            }
        }

        private void OnDisable()
        {
            if (_gameManager != null)
            {
                _gameManager.OnKillCountChanged -= HandleKillCountChanged;
            }
        }

        private void HandleKillCountChanged(int newCount)
        {
            if (_label == null) return;
            _label.text = string.Format(_format, newCount);
        }
    }
}