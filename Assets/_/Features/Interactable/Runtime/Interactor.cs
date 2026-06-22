using UnityEngine;

namespace Interactable.Runtime
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Interactor : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleEnter(other);
        }

        private void HandleEnter(Collider2D other)
        {
            Interactable interactable = other.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(gameObject);
            }
        }
    }
}