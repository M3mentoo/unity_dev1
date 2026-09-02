using UnityEngine;

namespace Tutorial.Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class EnemyPatrol : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _moveSpeed = 2f;
        [SerializeField, Min(0.01f)] private float _patrolDistance = 2f;

        private Rigidbody2D _rigidbody;
        private float _startX;
        private float _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _startX = _rigidbody.position.x;
            _direction = 1f;
        }

        private void OnDisable()
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = new Vector2(
                    0f,
                    _rigidbody.velocity.y);
            }
        }

        private void FixedUpdate()
        {
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
