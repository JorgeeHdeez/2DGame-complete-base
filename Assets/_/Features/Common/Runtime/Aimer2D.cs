using UnityEngine;

namespace Common.Runtime
{
    public class Aimer2D : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;

        private void Awake()
        {
            if (_pivot == null)
            {
                _pivot = transform;
            }
        }

        public void AimAt(Vector3 worldPosition)
        {
            Vector2 direction = (Vector2)(worldPosition - _pivot.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _pivot.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public Vector2 GetAimDirection()
        {
            return _pivot.right;
        }
    }
}
