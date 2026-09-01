using UnityEngine;

namespace Tutorial.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerRespawn : MonoBehaviour
    {
        [SerializeField] private Transform _respawnPoint;
        [SerializeField] private float _fallThreshold = -8f;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_respawnPoint == null || _rigidbody.position.y > _fallThreshold)
            {
                return;
            }

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            _rigidbody.position = _respawnPoint.position;
        }
    }
}
