using UnityEngine;

namespace Managers.Runtime
{
    [CreateAssetMenu(fileName = "GameRunData", menuName = "Managers/Game Run Data")]
    public class GameRunData : ScriptableObject
    {
        [SerializeField] private int _killCount;
        [SerializeField] private float _elapsedSeconds;
        [SerializeField] private int _coinCount;

        public int KillCount => _killCount;
        public float ElapsedSeconds => _elapsedSeconds;
        public int CoinCount => _coinCount;

        public void Record(int killCount, float elapsedSeconds)
        {
            _killCount = killCount;
            _elapsedSeconds = elapsedSeconds;
        }

        public void RecordCoins(int coinCount)
        {
            _coinCount = coinCount;
        }

        public void Reset()
        {
            _killCount = 0;
            _elapsedSeconds = 0f;
            _coinCount = 0;
        }
    }
}