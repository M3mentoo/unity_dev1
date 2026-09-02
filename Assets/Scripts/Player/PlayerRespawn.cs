using Tutorial.Combat;
using UnityEngine;

namespace Tutorial.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    public sealed class PlayerRespawn : MonoBehaviour
    {
        [SerializeField] private Transform _respawnPoint;
        [SerializeField] private float _fallThreshold = -8f;

        private Rigidbody2D _rigidbody;
        private Health _health;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.Died += Respawn;
        }

        private void OnDisable()
        {
            _health.Died -= Respawn;
        }

        private void FixedUpdate()
        {
            if (_respawnPoint == null || _rigidbody.position.y > _fallThreshold)
            {
                return;
            }

            Respawn();
        }

        private void Respawn()
        {
            if (_respawnPoint == null)
            {
                return;
            }

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            _rigidbody.position = _respawnPoint.position;
            _health.RestoreFullHealth();
        }
    }
}
