using UnityEngine;

namespace Tutorial.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerDash))]
    public sealed class PlayerWallSlide : MonoBehaviour
    {
        public bool IsWallSliding { get; private set; }
        public int WallDirection { get; private set; }

        [SerializeField] private LayerMask _wallLayer;
        [SerializeField, Min(0.01f)] private float _wallCheckDistance = 0.1f;
        [SerializeField, Min(0.01f)] private float _wallSlideSpeed = 3f;

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;
        private PlayerMovement _movement;
        private PlayerDash _dash;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _movement = GetComponent<PlayerMovement>();
            _dash = GetComponent<PlayerDash>();
        }

        private void OnDisable()
        {
            IsWallSliding = false;
            WallDirection = 0;
        }

        private void FixedUpdate()
        {
            WallDirection = FindWallDirection();
            IsWallSliding = WallDirection != 0
                && !_movement.IsGrounded()
                && !_dash.IsDashing
                && _rigidbody.velocity.y < 0f;

            if (!IsWallSliding)
            {
                return;
            }

            _rigidbody.velocity = new Vector2(
                _rigidbody.velocity.x,
                Mathf.Max(_rigidbody.velocity.y, -_wallSlideSpeed));
        }

        private int FindWallDirection()
        {
            Bounds bounds = _collider.bounds;
            float rayDistance = bounds.extents.x + _wallCheckDistance;

            if (Physics2D.Raycast(
                    bounds.center,
                    Vector2.right,
                    rayDistance,
                    _wallLayer))
            {
                return 1;
            }

            if (Physics2D.Raycast(
                    bounds.center,
                    Vector2.left,
                    rayDistance,
                    _wallLayer))
            {
                return -1;
            }

            return 0;
        }

        private void OnDrawGizmosSelected()
        {
            BoxCollider2D playerCollider = _collider != null
                ? _collider
                : GetComponent<BoxCollider2D>();

            if (playerCollider == null)
            {
                return;
            }

            Bounds bounds = playerCollider.bounds;
            float rayDistance = bounds.extents.x + _wallCheckDistance;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(
                bounds.center,
                bounds.center + Vector3.right * rayDistance);
            Gizmos.DrawLine(
                bounds.center,
                bounds.center + Vector3.left * rayDistance);
        }
    }
}
