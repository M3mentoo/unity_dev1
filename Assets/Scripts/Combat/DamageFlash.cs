using System.Collections;
using UnityEngine;

namespace Tutorial.Combat
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer), typeof(Health))]
    public sealed class DamageFlash : MonoBehaviour
    {
        [SerializeField] private Color _flashColor = new Color(1f, 0.35f, 0.35f, 1f);
        [SerializeField, Min(0f)] private float _flashDuration = 0.12f;

        private SpriteRenderer _spriteRenderer;
        private Health _health;
        private Coroutine _flashRoutine;
        private Color _baseColor;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _baseColor = _spriteRenderer.color;
            _health.Damaged += HandleDamaged;
        }

        private void OnDisable()
        {
            _health.Damaged -= HandleDamaged;

            if (_flashRoutine != null)
            {
                StopCoroutine(_flashRoutine);
                _flashRoutine = null;
            }

            _spriteRenderer.color = _baseColor;
        }

        private void HandleDamaged(DamageInfo _)
        {
            if (_flashRoutine != null)
            {
                StopCoroutine(_flashRoutine);
            }

            _flashRoutine = StartCoroutine(Flash());
        }

        private IEnumerator Flash()
        {
            _spriteRenderer.color = _flashColor;
            yield return new WaitForSeconds(_flashDuration);
            _spriteRenderer.color = _baseColor;
            _flashRoutine = null;
        }
    }
}
