using System;
using System.Collections.Generic;
using Managers.Runtime;
using Spawner.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Runtime
{
    [RequireComponent(typeof(CircleSpawnPoint))]
    public class EnemyManager : MonoBehaviour, IUpdatable
    {
        public event Action<int> OnActiveCountChanged;
        
        [Header("Spawn Configuration")]
        [SerializeField] private List<EnemySpawnEntry> _spawnEntries = new List<EnemySpawnEntry>();
        [SerializeField] private int _globalMaxEnemies = 60;
        [SerializeField] private Transform _poolsParent;

        [Header("References")]
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private UpdateManager _updateManager;
        [SerializeField] private Transform _target;

        [Header("Difficulty")]
        [SerializeField] private float _baseSpawnInterval = 1f;
        [SerializeField] private float _minSpawnInterval = 0.1f;
        [SerializeField] private float _difficultyStep = 0.02f;
       
        public int ActiveEnemyCount => _totalActiveCount;

        private CircleSpawnPoint _spawnPoint;
        private List<ObjectPool> _pools = new List<ObjectPool>();
        private float _timer;
        private int _totalActiveCount;

        private void Awake()
        {
            _spawnPoint = GetComponent<CircleSpawnPoint>();
            CreatePools();
        }

        private void OnEnable()
        {
            if (_updateManager != null) _updateManager.Register(this);
            if (_gameManager != null) _gameManager.OnGameOver += HandleGameOver;
        }

        private void OnDisable()
        {
            if (_updateManager != null) _updateManager.Unregister(this);
            if (_gameManager != null) _gameManager.OnGameOver -= HandleGameOver;
        }

        public void OnTick(float deltaTime)
        {
            HandleSpawnTimer(deltaTime);
        }

        private void CreatePools()
        {
            Transform parent = _poolsParent != null ? _poolsParent : transform;

            for (int i = 0; i < _spawnEntries.Count; i++)
            {
                EnemySpawnEntry entry = _spawnEntries[i];
                if (entry.Prefab == null) continue;

                GameObject poolHolder = new GameObject($"Pool_{entry.Prefab.name}");
                poolHolder.transform.SetParent(parent);

                ObjectPool pool = poolHolder.AddComponent<ObjectPool>();
                pool.Configure(entry.Prefab, entry.InitialPoolSize, _globalMaxEnemies, poolHolder.transform);

                _pools.Add(pool);
            }
        }

        private void HandleSpawnTimer(float deltaTime)
        {
            if (_gameManager != null && _gameManager.IsGameOver) return;
            if (_totalActiveCount >= _globalMaxEnemies)
            {
                _timer = 0f;
                return;
            }

            _timer += deltaTime;
            float currentInterval = ComputeCurrentInterval();

            if (_timer >= currentInterval)
            {
                _timer = 0f;
                SpawnEnemy();
            }
        }

        private float ComputeCurrentInterval()
        {
            int kills = _gameManager != null ? _gameManager.KillCount : 0;
            float interval = _baseSpawnInterval - kills * _difficultyStep;
            return Mathf.Max(_minSpawnInterval, interval);
        }

        private void SpawnEnemy()
        {
            ObjectPool selectedPool = SelectRandomPool();
            if (selectedPool == null) return;

            Vector2 position = _spawnPoint.GetRandomPoint();
            GameObject enemy = selectedPool.Get(position, Quaternion.identity);
            if (enemy == null) return;

            ConfigureEnemy(enemy);
            _totalActiveCount++;
            OnActiveCountChanged?.Invoke(_totalActiveCount);
        }

        private ObjectPool SelectRandomPool()
        {
            if (_spawnEntries.Count == 0 || _pools.Count == 0) return null;

            float totalWeight = 0f;
            for (int i = 0; i < _spawnEntries.Count; i++)
            {
                totalWeight += Mathf.Max(0f, _spawnEntries[i].Weight);
            }

            if (totalWeight <= 0f) return null;

            float roll = Random.Range(0f, totalWeight);
            float cumulative = 0f;

            for (int i = 0; i < _spawnEntries.Count; i++)
            {
                cumulative += Mathf.Max(0f, _spawnEntries[i].Weight);
                if (roll <= cumulative)
                {
                    return i < _pools.Count ? _pools[i] : null;
                }
            }

            return null;
        }

        private void ConfigureEnemy(GameObject enemy)
        {
            Health.Runtime.Health health = enemy.GetComponent<Health.Runtime.Health>();
            if (health != null)
            {
                health.ResetHealth();
            }

            TargetFollower follower = enemy.GetComponent<TargetFollower>();
            if (follower != null)
            {
                if (_target != null) follower.SetTarget(_target);
                follower.SetUpdateManager(_updateManager);
            }

            EnemyBrain brain = enemy.GetComponent<EnemyBrain>();
            if (brain != null)
            {
                if (_target != null) brain.SetTarget(_target);
                brain.SetUpdateManager(_updateManager);
            }

            ReleaseOnDeath release = enemy.GetComponent<ReleaseOnDeath>();
            if (release != null)
            {
                release.SetManager(this);
            }
        }

        public void ReleaseEnemy(GameObject enemy)
        {
            if (enemy == null) return;

            ObjectPool ownerPool = FindOwnerPool(enemy);
            if (ownerPool != null)
            {
                ownerPool.Release(enemy);
                _totalActiveCount = Mathf.Max(0, _totalActiveCount - 1);
                OnActiveCountChanged?.Invoke(_totalActiveCount);
            }

            if (_gameManager != null)
            {
                _gameManager.RegisterKill();
            }
        }

        private ObjectPool FindOwnerPool(GameObject enemy)
        {
            for (int i = 0; i < _pools.Count; i++)
            {
                ObjectPool pool = _pools[i];
                if (pool != null && enemy.transform.parent == pool.transform)
                {
                    return pool;
                }
            }
            return null;
        }

        private void HandleGameOver()
        {
            for (int i = 0; i < _pools.Count; i++)
            {
                if (_pools[i] != null)
                {
                    _pools[i].ReleaseAll();
                }
            }
            _totalActiveCount = 0;
            OnActiveCountChanged?.Invoke(_totalActiveCount);
        }
    }
}