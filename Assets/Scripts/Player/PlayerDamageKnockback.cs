using Tutorial.Combat;
using UnityEngine;

namespace Tutorial.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerMovement), typeof(Health))]
    public sealed class PlayerDamageKnockback : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _horizontalSpeed = 6f;
        [SerializeField, Min(0f)] private float _upwardSpeed = 5f;
        [SerializeField, Min(0f)] private float _controlLockDuration = 0.25f;

        private PlayerMovement _movement;
        private Health _health;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.Damaged += HandleDamaged;
        }

        private void OnDisable()
        {
            _health.Damaged -= HandleDamaged;
        }

        private void HandleDamaged(DamageInfo damage)
        {
            float horizontalDirection = Mathf.Approximately(
                damage.Direction.x,
                0f)
                ? 0f
                : Mathf.Sign(damage.Direction.x);

            Vector2 launchVelocity = new Vector2(
                horizontalDirection * _horizontalSpeed,
                _upwardSpeed);

            _movement.Launch(
                launchVelocity,
                _controlLockDuration);
        }
    }
}
