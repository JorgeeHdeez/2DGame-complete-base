using UnityEngine;

namespace Common.Runtime
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover2D : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;

        private Rigidbody2D _rb;
        private Vector2 _direction;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            _rb.linearVelocity = _direction * _speed;
        }

        public void SetDirection(Vector2 newDirection)
        {
            _direction = newDirection;
        }

        public void SetSpeed(float newSpeed)
        {
            _speed = newSpeed;
        }
    }
}