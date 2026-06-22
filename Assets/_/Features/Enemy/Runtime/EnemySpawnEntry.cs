using System;
using UnityEngine;

namespace Enemy.Runtime
{
    [Serializable]
    public struct EnemySpawnEntry
    {
        public GameObject Prefab;
        public int InitialPoolSize;
        public float Weight;
    }
}