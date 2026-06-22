using UnityEngine;

namespace Common.Runtime
{
    [CreateAssetMenu(fileName = "AudioPoolReference", menuName = "Audio/Audio Pool Reference")]
    public class AudioPoolReference : ScriptableObject
    {
        private AudioPool _runtimePool;

        public AudioPool Pool => _runtimePool;

        public void Register(AudioPool pool)
        {
            _runtimePool = pool;
        }

        public void Unregister(AudioPool pool)
        {
            if (_runtimePool == pool) _runtimePool = null;
        }
    }
}