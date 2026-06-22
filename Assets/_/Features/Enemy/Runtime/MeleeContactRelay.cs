using UnityEngine;

namespace Enemy.Runtime
{
    public class MeleeContactRelay : MonoBehaviour
    {
        private MeleeContactDetector _detector;

        public void SetDetector(MeleeContactDetector detector)
        {
            _detector = detector;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_detector != null) _detector.HandleEnter(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_detector != null) _detector.HandleExit(other);
        }
    }
}