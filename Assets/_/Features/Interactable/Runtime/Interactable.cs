using System;
using UnityEngine;

namespace Interactable.Runtime
{
    public class Interactable : MonoBehaviour
    {
        public event Action<GameObject> OnInteract;

        public void Interact(GameObject interactor)
        {
            OnInteract?.Invoke(interactor);
        }
    }
}