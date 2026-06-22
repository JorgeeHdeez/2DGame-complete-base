// using UnityEngine;
//
// namespace Health.Runtime
// {
//     [RequireComponent(typeof(Health))]
//     public class DestroyOnDeath : MonoBehaviour
//     {
//         [SerializeField] private float _delay = 0f;
//
//         private Health health;
//
//         private void Awake()
//         {
//             health = GetComponent<Health>();
//         }
//
//         private void OnEnable() => health.OnDeath += HandleDeath;
//       
//         private void OnDisable() => health.OnDeath -= HandleDeath;
//        
//         private void HandleDeath()
//         {
//             Destroy(gameObject, _delay);
//         }
//     }
// }
