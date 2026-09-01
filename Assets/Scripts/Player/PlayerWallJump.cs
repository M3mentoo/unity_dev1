using UnityEngine;

namespace Tutorial.Player
{
    [RequireComponent(
        typeof(PlayerMovement),
        typeof(PlayerWallSlide),
        typeof(PlayerDash))]
    public sealed class PlayerWallJump : MonoBehaviour
    {
        [SerializeField, Min(0.01f)] private float _horizontalJumpSpeed = 7f;
        [SerializeField, Min(0.01f)] private float _verticalJumpSpeed = 9f;
        [SerializeField, Min(0f)] private float _horizontalControlLockTime = 0.15f;

        private PlayerMovement _movement;
        private PlayerWallSlide _wallSlide;
        private PlayerDash _dash;
        private bool _jumpRequested;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _wallSlide = GetComponent<PlayerWallSlide>();
            _dash = GetComponent<PlayerDash>();
        }

        private void OnDisable()
        {
            _jumpRequested = false;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                _jumpRequested = true;
            }
        }

        private void FixedUpdate()
        {
            if (!_jumpRequested)
            {
                return;
            }

            _jumpRequested = false;

            if (_dash.IsDashing
                || _movement.IsGrounded()
                || !_wallSlide.IsTouchingWall)
            {
                return;
            }

            Vector2 launchVelocity = new Vector2(
                -_wallSlide.WallDirection * _horizontalJumpSpeed,
                _verticalJumpSpeed);

            _movement.Launch(
                launchVelocity,
                _horizontalControlLockTime);
        }
    }
}
