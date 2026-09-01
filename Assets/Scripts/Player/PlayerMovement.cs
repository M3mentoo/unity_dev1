using UnityEngine;

namespace Tutorial.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _moveSpeed = 6f;
        [SerializeField, Min(0f)] private float _jumpSpeed = 9f;
        [SerializeField, Min(0f)] private float _coyoteTime = 0.1f;
        [SerializeField, Min(0f)] private float _jumpBufferTime = 0.1f;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField, Min(0f)] private float _groundCheckRadius = 0.15f;

        private Rigidbody2D _rigidbody;
        private float _horizontalInput;
        private float _coyoteTimeRemaining;
        private float _jumpBufferTimeRemaining;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                _jumpBufferTimeRemaining = _jumpBufferTime;
            }
        }

        private void FixedUpdate()
        {
            UpdateCoyoteTimer(IsGrounded());

            _rigidbody.velocity = new Vector2(
                _horizontalInput * _moveSpeed,
                _rigidbody.velocity.y);

            if (_jumpBufferTimeRemaining > 0f && _coyoteTimeRemaining > 0f)
            {
                _rigidbody.velocity = new Vector2(
                    _rigidbody.velocity.x,
                    _jumpSpeed);

                _jumpBufferTimeRemaining = 0f;
                _coyoteTimeRemaining = 0f;
            }

            _jumpBufferTimeRemaining = Mathf.Max(
                0f,
                _jumpBufferTimeRemaining - Time.fixedDeltaTime);
        }

        private void UpdateCoyoteTimer(bool isGrounded)
        {
            _coyoteTimeRemaining = isGrounded
                ? _coyoteTime
                : Mathf.Max(0f, _coyoteTimeRemaining - Time.fixedDeltaTime);
        }

        private bool IsGrounded()
        {
            return _groundCheck != null
                && Physics2D.OverlapCircle(
                    _groundCheck.position,
                    _groundCheckRadius,
                    _groundLayer) != null;
        }

        private void OnDrawGizmosSelected()
        {
            if (_groundCheck == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }
}
