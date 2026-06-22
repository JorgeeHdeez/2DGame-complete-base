using UnityEngine;

namespace Spawner.Runtime
{
    public class CircleSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _center;
        [SerializeField] private float _radius = 8f;

        public Vector2 GetRandomPoint()
        {
            if (_center == null)
            {
                return Vector2.zero;
            }

            float angle = Random.Range(0f, Mathf.PI * 2f);
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _radius;
            return (Vector2)_center.position + offset;
        }

        public void SetCenter(Transform newCenter)
        {
            _center = newCenter;
        }

        private void OnDrawGizmosSelected()
        {
            if (_center == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_center.position, _radius);
        }
    }
}
