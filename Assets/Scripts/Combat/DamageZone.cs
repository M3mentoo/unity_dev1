using UnityEngine;

namespace Tutorial.Combat
{
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class DamageZone : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _damage = 1;

        private BoxCollider2D _collider;

        private void Reset()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();

            if (!_collider.isTrigger)
            {
                Debug.LogWarning(
                    $"{nameof(DamageZone)} on {name} requires Is Trigger to be enabled.",
                    this);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            Health health = other.GetComponentInParent<Health>();

            if (health != null)
            {
                Vector2 damageDirection =
                    (Vector2)health.transform.position
                    - (Vector2)transform.position;

                health.TryTakeDamage(
                    new DamageInfo(_damage, damageDirection));
            }
        }
    }
}
