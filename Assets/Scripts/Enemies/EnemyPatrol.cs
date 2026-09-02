using Tutorial.Combat;
using UnityEngine;

namespace Tutorial.Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D), typeof(Health))]
    public sealed class EnemyPatrol : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _moveSpeed = 2f;
        [SerializeField, Min(0.01f)] private float _patrolDistance = 2f;
        [SerializeField, Min(0f)] private float _hitStunDuration = 0.15f;
        [SerializeField, Min(0f)] private float _knockbackSpeed = 4f;
        [SerializeField, Min(0f)] private float _knockbackUpwardSpeed = 2f;

        private Rigidbody2D _rigidbody;
        private Health _health;
        private float _startX;
        private float _direction;
        private float _hitStunTimeRemaining;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _startX = _rigidbody.position.x;
            _direction = 1f;
            _health.Damaged += HandleDamaged;
        }

        private void OnDisable()
        {
            _health.Damaged -= HandleDamaged;
            _hitStunTimeRemaining = 0f;

            if (_rigidbody != null)
            {
                _rigidbody.velocity = new Vector2(
                    0f,
                    _rigidbody.velocity.y);
            }
        }

        private void FixedUpdate()
        {
            if (_hitStunTimeRemaining > 0f)
            {
                _hitStunTimeRemaining = Mathf.Max(
                    0f,
                    _hitStunTimeRemaining - Time.fixedDeltaTime);
                return;
            }

            float distanceFromStart = _rigidbody.position.x - _startX;

            if (distanceFromStart >= _patrolDistance)
            {
                _direction = -1f;
            }
            else if (distanceFromStart <= -_patrolDistance)
            {
                _direction = 1f;
            }

            _rigidbody.velocity = new Vector2(
                _direction * _moveSpeed,
                _rigidbody.velocity.y);
        }

        private void HandleDamaged(DamageInfo damage)
        {
            _hitStunTimeRemaining = _hitStunDuration;

            float horizontalDirection = Mathf.Approximately(
                damage.Direction.x,
                0f)
                ? 0f
                : Mathf.Sign(damage.Direction.x);
            _rigidbody.velocity = new Vector2(
                horizontalDirection * _knockbackSpeed,
                _knockbackUpwardSpeed);
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 center = transform.position;
            Vector3 left = center + Vector3.left * _patrolDistance;
            Vector3 right = center + Vector3.right * _patrolDistance;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(left, right);
            Gizmos.DrawWireSphere(left, 0.1f);
            Gizmos.DrawWireSphere(right, 0.1f);
        }
    }
}
