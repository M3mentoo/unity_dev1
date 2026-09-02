using System.Collections.Generic;
using Tutorial.Combat;
using UnityEngine;

namespace Tutorial.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerMovement), typeof(Health))]
    public sealed class PlayerAttack : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _attackDamage = 1;
        [SerializeField, Min(0f)] private float _attackCooldown = 0.35f;
        [SerializeField] private Vector2 _attackOffset = new Vector2(0.8f, 0f);
        [SerializeField] private Vector2 _attackSize = new Vector2(1.2f, 1f);
        [SerializeField] private LayerMask _targetLayers;

        private readonly HashSet<Health> _damagedTargets = new HashSet<Health>();

        private PlayerMovement _movement;
        private Health _ownHealth;
        private float _nextAttackTime;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _ownHealth = GetComponent<Health>();
        }

        private void Update()
        {
            if (!Input.GetButtonDown("Fire1") || Time.time < _nextAttackTime)
            {
                return;
            }

            PerformAttack();
            _nextAttackTime = Time.time + _attackCooldown;
        }

        private void PerformAttack()
        {
            Vector2 attackCenter = GetAttackCenter(_movement.FacingDirection);
            Collider2D[] hits = Physics2D.OverlapBoxAll(
                attackCenter,
                _attackSize,
                0f,
                _targetLayers);

            _damagedTargets.Clear();

            foreach (Collider2D hit in hits)
            {
                Health targetHealth = hit.GetComponentInParent<Health>();

                if (targetHealth == null
                    || targetHealth == _ownHealth
                    || !_damagedTargets.Add(targetHealth))
                {
                    continue;
                }

                targetHealth.TryTakeDamage(_attackDamage);
            }
        }

        private Vector2 GetAttackCenter(float facingDirection)
        {
            Vector2 directionalOffset = new Vector2(
                _attackOffset.x * facingDirection,
                _attackOffset.y);

            return (Vector2)transform.position + directionalOffset;
        }

        private void OnDrawGizmosSelected()
        {
            float facingDirection = _movement != null
                ? _movement.FacingDirection
                : 1f;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                GetAttackCenter(facingDirection),
                _attackSize);
        }
    }
}
