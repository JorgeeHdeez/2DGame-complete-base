using UnityEngine;

namespace Common.Runtime
{
    public class Lifetime : MonoBehaviour
    {
        [SerializeField] private float _duration = 15f;
        [SerializeField] private float _blinkDuration = 2f;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _timer;
        private bool _isBlinking;
        private float _blinkTimer;

        private void Awake()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        private void Update()
        {
            HandleLifetime();
            HandleBlinking();
        }

        private void HandleLifetime()
        {
            _timer += Time.deltaTime;

            if (!_isBlinking && _timer >= _duration - _blinkDuration)
            {
                _isBlinking = true;
            }

            if (_timer >= _duration)
            {
                Destroy(gameObject);
            }
        }

        private void HandleBlinking()
        {
            if (!_isBlinking || _spriteRenderer == null) return;

            _blinkTimer += Time.deltaTime;
            bool visible = Mathf.FloorToInt(_blinkTimer * 8f) % 2 == 0;
            _spriteRenderer.enabled = visible;
        }
    }
}