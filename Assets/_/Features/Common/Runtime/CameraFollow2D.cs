using UnityEngine;

namespace Common.Runtime
{
    public class CameraFollow2D : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Transform _target;

        [Header("Dead Zone")]
        [SerializeField] private Vector2 _deadZoneSize = new Vector2(2f, 1.5f);

        [Header("Smoothing")]
        [SerializeField] private float _smoothTime = 0.2f;
        [SerializeField] private float _maxSpeed = 50f;

        [Header("Bounds")]
        [SerializeField] private bool _useBounds = false;
        [SerializeField] private Vector2 _minBounds = new Vector2(-20f, -10f);
        [SerializeField] private Vector2 _maxBounds = new Vector2(20f, 10f);

        private Vector2 _velocity;

        private void LateUpdate()
        {
            HandleFollow();
        }

        private void HandleFollow()
        {
            if (_target == null) return;

            Vector2 cameraPos = transform.position;
            Vector2 targetPos = _target.position;

            Vector2 desiredPos = ComputeDesiredPosition(cameraPos, targetPos);
            Vector2 smoothedPos = Vector2.SmoothDamp(cameraPos, desiredPos, ref _velocity, _smoothTime, _maxSpeed);

            if (_useBounds)
            {
                smoothedPos = ClampToBounds(smoothedPos);
            }

            transform.position = new Vector3(smoothedPos.x, smoothedPos.y, transform.position.z);
        }

        private Vector2 ComputeDesiredPosition(Vector2 cameraPos, Vector2 targetPos)
        {
            Vector2 delta = targetPos - cameraPos;
            Vector2 halfDeadZone = _deadZoneSize * 0.5f;
            Vector2 result = cameraPos;

            if (delta.x > halfDeadZone.x)
            {
                result.x = targetPos.x - halfDeadZone.x;
            }
            else if (delta.x < -halfDeadZone.x)
            {
                result.x = targetPos.x + halfDeadZone.x;
            }

            if (delta.y > halfDeadZone.y)
            {
                result.y = targetPos.y - halfDeadZone.y;
            }
            else if (delta.y < -halfDeadZone.y)
            {
                result.y = targetPos.y + halfDeadZone.y;
            }

            return result;
        }

        private Vector2 ClampToBounds(Vector2 position)
        {
            return new Vector2(
                Mathf.Clamp(position.x, _minBounds.x, _maxBounds.x),
                Mathf.Clamp(position.y, _minBounds.y, _maxBounds.y)
            );
        }

        public void SetTarget(Transform newTarget)
        {
            _target = newTarget;
        }

        private void OnDrawGizmos()
        {
            DrawDeadZone();
            DrawBounds();
        }

        private void DrawDeadZone()
        {
            Gizmos.color = Color.yellow;
            Vector3 center = transform.position;
            Vector3 size = new Vector3(_deadZoneSize.x, _deadZoneSize.y, 0f);
            Gizmos.DrawWireCube(center, size);
        }

        private void DrawBounds()
        {
            if (!_useBounds) return;

            Gizmos.color = Color.cyan;
            Vector3 center = new Vector3(
                (_minBounds.x + _maxBounds.x) * 0.5f,
                (_minBounds.y + _maxBounds.y) * 0.5f,
                0f
            );
            Vector3 size = new Vector3(
                _maxBounds.x - _minBounds.x,
                _maxBounds.y - _minBounds.y,
                0f
            );
            Gizmos.DrawWireCube(center, size);
        }
    }
}