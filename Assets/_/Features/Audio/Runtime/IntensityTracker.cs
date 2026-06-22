using System;
using Enemy.Runtime;
using UnityEngine;

namespace Audio.Runtime
{
    public class IntensityTracker : MonoBehaviour
    {
        [SerializeField] private EnemyManager _enemyManager;

        [Header("Combat Thresholds (Active Enemies)")]
        [SerializeField] private int _combatEnterThreshold = 8;
        [SerializeField] private int _combatExitThreshold = 3;

        [Header("Intense Thresholds (Active Enemies)")]
        [SerializeField] private int _intenseEnterThreshold = 25;
        [SerializeField] private int _intenseExitThreshold = 18;

        public event Action<MusicIntensity> OnIntensityChanged;

        private MusicIntensity _currentIntensity = MusicIntensity.Calm;

        public MusicIntensity CurrentIntensity => _currentIntensity;

        private void OnEnable()
        {
            if (_enemyManager != null)
            {
                _enemyManager.OnActiveCountChanged += HandleActiveCountChanged;
                Evaluate(_enemyManager.ActiveEnemyCount);
            }
        }

        private void OnDisable()
        {
            if (_enemyManager != null)
            {
                _enemyManager.OnActiveCountChanged -= HandleActiveCountChanged;
            }
        }

        private void HandleActiveCountChanged(int newCount)
        {
            Evaluate(newCount);
        }

        private void Evaluate(int activeCount)
        {
            MusicIntensity newIntensity = ComputeIntensity(activeCount);
            if (newIntensity != _currentIntensity)
            {
                _currentIntensity = newIntensity;
                OnIntensityChanged?.Invoke(_currentIntensity);
            }
        }

        private MusicIntensity ComputeIntensity(int activeCount)
        {
            switch (_currentIntensity)
            {
                case MusicIntensity.Calm:
                    if (activeCount >= _intenseEnterThreshold) return MusicIntensity.Intense;
                    if (activeCount >= _combatEnterThreshold) return MusicIntensity.Combat;
                    return MusicIntensity.Calm;

                case MusicIntensity.Combat:
                    if (activeCount >= _intenseEnterThreshold) return MusicIntensity.Intense;
                    if (activeCount <= _combatExitThreshold) return MusicIntensity.Calm;
                    return MusicIntensity.Combat;

                case MusicIntensity.Intense:
                    if (activeCount <= _intenseExitThreshold) return MusicIntensity.Combat;
                    return MusicIntensity.Intense;

                default:
                    return MusicIntensity.Calm;
            }
        }
    }
}