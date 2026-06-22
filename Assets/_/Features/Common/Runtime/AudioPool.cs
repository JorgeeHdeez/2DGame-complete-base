using System.Collections.Generic;
using UnityEngine;

namespace Common.Runtime
{
    public class AudioPool : MonoBehaviour
    {
        [SerializeField] private int _poolSize = 16;
        [SerializeField] private AudioPoolReference _reference;

        private readonly List<AudioSource> _sources = new List<AudioSource>();
        private int _nextIndex;

        private void Awake()
        {
            CreateSources();
        }

        private void OnEnable()
        {
            if (_reference != null) _reference.Register(this);
        }

        private void OnDisable()
        {
            if (_reference != null) _reference.Unregister(this);
        }

        private void CreateSources()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject sourceHolder = new GameObject($"AudioSource_{i}");
                sourceHolder.transform.SetParent(transform);

                AudioSource source = sourceHolder.AddComponent<AudioSource>();
                source.playOnAwake = false;
                source.spatialBlend = 0f;

                _sources.Add(source);
            }
        }

        public void PlayClip(AudioClip clip, Vector3 position, float volume = 1f)
        {
            if (clip == null) return;
            if (_sources.Count == 0) return;

            AudioSource source = GetNextAvailableSource();
            source.transform.position = position;
            source.clip = clip;
            source.volume = Mathf.Clamp01(volume);
            source.Play();
        }

        private AudioSource GetNextAvailableSource()
        {
            for (int i = 0; i < _sources.Count; i++)
            {
                int index = (_nextIndex + i) % _sources.Count;
                if (!_sources[index].isPlaying)
                {
                    _nextIndex = (index + 1) % _sources.Count;
                    return _sources[index];
                }
            }

            AudioSource fallback = _sources[_nextIndex];
            _nextIndex = (_nextIndex + 1) % _sources.Count;
            return fallback;
        }
    }
}