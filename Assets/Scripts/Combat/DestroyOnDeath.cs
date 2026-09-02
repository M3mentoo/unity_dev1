using UnityEngine;

namespace Tutorial.Combat
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public sealed class DestroyOnDeath : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.Died += HandleDied;
        }

        private void OnDisable()
        {
            _health.Died -= HandleDied;
        }

        private void HandleDied()
        {
            Destroy(gameObject);
        }
    }
}
