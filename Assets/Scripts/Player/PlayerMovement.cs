using UnityEngine;

namespace Tutorial.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _moveSpeed = 6f;

        private Rigidbody2D _rigidbody;
        private float _horizontalInput;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(
                _horizontalInput * _moveSpeed,
                _rigidbody.velocity.y);
        }
    }
}
