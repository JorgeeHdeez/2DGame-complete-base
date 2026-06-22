using Common.Runtime;
using Managers.Runtime;
using UnityEngine;
using Projectile.Runtime;
using UnityEngine.EventSystems;

namespace Player.Runtime
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PauseManager _pauseManager;
        [SerializeField] private Mover2D _mover;
        [SerializeField] private Aimer2D _aimer;
        [SerializeField] private Shooter _shooter;
        [SerializeField] private Ammo _ammo;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private KeyCode _reloadKey = KeyCode.R;
        [SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;

        private void Awake()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
        }

        private void Update()
        {
            if (_pauseManager != null && _pauseManager.IsPaused) return;
            
            HandleMovementInput();
            HandleAimInput();
            HandleShootInput();
            HandleReloadInput();
        }

        private void HandleMovementInput()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector2 direction = new Vector2(x, y).normalized;
            _mover.SetDirection(direction);
        }

        private void HandleAimInput()
        {
            Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _aimer.AimAt(mouseWorld);
        }

        private void HandleShootInput()
        {
            if (IsPointerOverUI()) return;

            if (Input.GetKey(_shootKey))
            {
                if (_ammo.HasAmmo() && _shooter.TryShoot())
                {
                    _ammo.Consume();
                }
            }
        }

        private bool IsPointerOverUI()
        {
            if (EventSystem.current == null) return false;
            return EventSystem.current.IsPointerOverGameObject();
        }

        private void HandleReloadInput()
        {
            if (Input.GetKeyDown(_reloadKey))
            {
                _ammo.Reload();
            }
        }
    }
}
