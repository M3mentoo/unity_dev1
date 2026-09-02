using UnityEngine;

namespace Tutorial.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerMovement))]
    public sealed class PlayerDash : MonoBehaviour
    {
        public bool IsDashing => _isDashing;

        [SerializeField] private KeyCode _dashKey = KeyCode.LeftShift;
        [SerializeField, Min(0.01f)] private float _dashSpeed = 12f;
        [SerializeField, Min(0.01f)] private float _dashDuration = 0.15f;
        [SerializeField, Min(0f)] private float _dashCooldown = 0.5f;

        private Rigidbody2D _rigidbody;
        private PlayerMovement _movement;
        private Vector2 _activeDashDirection;
        private float _dashTimeRemaining;
        private float _cooldownTimeRemaining;
        private float _savedGravityScale;
        private bool _dashRequested;
        private bool _isDashing;
        private bool _airDashAvailable = true;
        private bool _movementWasEnabled;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _movement = GetComponent<PlayerMovement>();
        }

        private void OnDisable()
        {
            _dashRequested = false;

            if (_isDashing)
            {
                EndDash();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(_dashKey))
            {
                _dashRequested = true;
            }
        }

        private void FixedUpdate()
        {
            if (_isDashing)
            {
                ContinueDash();
                return;
            }

            bool isGrounded = _movement.IsGrounded();

            if (isGrounded)
            {
                _airDashAvailable = true;
            }

            _cooldownTimeRemaining = Mathf.Max(
                0f,
                _cooldownTimeRemaining - Time.fixedDeltaTime);

            if (_dashRequested
                && _cooldownTimeRemaining <= 0f
                && (isGrounded || _airDashAvailable))
            {
                BeginDash(isGrounded);
            }

            _dashRequested = false;
        }

        private void BeginDash(bool isGrounded)
        {
            _isDashing = true;
            _dashTimeRemaining = _dashDuration;
            _movementWasEnabled = _movement.enabled;
            _savedGravityScale = _rigidbody.gravityScale;
            _activeDashDirection = new Vector2(
                _movement.FacingDirection,
                0f);

            if (!isGrounded)
            {
                _airDashAvailable = false;
            }

            _movement.enabled = false;
            _rigidbody.gravityScale = 0f;
            _rigidbody.velocity = _activeDashDirection * _dashSpeed;
        }

        private void ContinueDash()
        {
            _rigidbody.velocity = _activeDashDirection * _dashSpeed;
            _dashTimeRemaining -= Time.fixedDeltaTime;

            if (_dashTimeRemaining <= 0f)
            {
                EndDash();
            }
        }

        private void EndDash()
        {
            _isDashing = false;
            _rigidbody.gravityScale = _savedGravityScale;
            _movement.enabled = _movementWasEnabled;
            _cooldownTimeRemaining = _dashCooldown;
        }
    }
}
