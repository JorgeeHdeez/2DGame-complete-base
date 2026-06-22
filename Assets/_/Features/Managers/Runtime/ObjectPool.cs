using System.Collections.Generic;
using UnityEngine;

namespace Managers.Runtime
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _initialSize = 20;
        [SerializeField] private int _maxSize = 50;
        [SerializeField] private Transform _parent;

        private readonly Queue<GameObject> _available = new Queue<GameObject>();
        private int _totalCount;

        public int ActiveCount => _totalCount - _available.Count;
        public int MaxSize => _maxSize;
        public bool IsFull => ActiveCount >= _maxSize;

        private void Awake()
        {
            if (_prefab == null) return;

            int safeInitial = Mathf.Min(_initialSize, _maxSize);
            for (int i = 0; i < safeInitial; i++)
            {
                CreateNewInstance();
            }
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            if (_available.Count == 0)
            {
                if (_totalCount >= _maxSize)
                {
                    Debug.LogWarning($"[ObjectPool] Pool is full ({_totalCount}/{_maxSize}). Cannot spawn new instance.");
                    return null;
                }
                CreateNewInstance();
            }

            if (_available.Count == 0)
            {
                return null;
            }

            GameObject instance = _available.Dequeue();
            instance.transform.SetPositionAndRotation(position, rotation);
            instance.SetActive(true);
            return instance;
        }
        public void Release(GameObject instance)
        {
            if (instance == null) return;
            instance.SetActive(false);
            _available.Enqueue(instance);
        }

        public void ReleaseAll()
        {
            Transform parentTransform = _parent != null ? _parent : transform;
            for (int i = parentTransform.childCount - 1; i >= 0; i--)
            {
                GameObject child = parentTransform.GetChild(i).gameObject;
                if (child.activeSelf)
                {
                    Release(child);
                }
            }
        }

        private void CreateNewInstance()
        {
            if (_prefab == null) return;
            if (_totalCount >= _maxSize) return;

            GameObject instance = Instantiate(_prefab, _parent);
            instance.SetActive(false);
            _available.Enqueue(instance);
            _totalCount++;
        }
        
        public void Configure(GameObject prefab, int initialSize, int maxSize, Transform parent)
        {
            _prefab = prefab;
            _initialSize = initialSize;
            _maxSize = maxSize;
            _parent = parent;

            _available.Clear();
            _totalCount = 0;

            int safeInitial = Mathf.Min(_initialSize, _maxSize);
            for (int i = 0; i < safeInitial; i++)
            {
                CreateNewInstance();
            }
        }
    }
}