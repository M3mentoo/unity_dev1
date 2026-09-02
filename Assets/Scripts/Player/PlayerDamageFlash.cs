using System.Collections;
using Tutorial.Combat;
using UnityEngine;

namespace Tutorial.Presentation
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer), typeof(Health))]
    public sealed class PlayerDamageFlash : MonoBehaviour
    {
        [SerializeField] private Color _flashColor = new Color(1f, 0.35f, 0.35f, 1f);
        [SerializeField, Min(0f)] private float _flashDuration = 0.12f;

        private SpriteRenderer _spriteRenderer;
        private Health _health;
        private Coroutine _flashRoutine;
        private Color _baseColor;
        private int _previousHealth;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _baseColor = _spriteRenderer.color;
            _previousHealth = _health.CurrentHealth;
            _health.HealthChanged += HandleHealthChanged;
        }

        private void OnDisable()
        {
            _health.HealthChanged -= HandleHealthChanged;

            if (_flashRoutine != null)
            {
                StopCoroutine(_flashRoutine);
                _flashRoutine = null;
            }

            _spriteRenderer.color = _baseColor;
        }

        private void HandleHealthChanged(int currentHealth, int _)
        {
            bool tookDamage = currentHealth < _previousHealth;
            _previousHealth = currentHealth;

            if (!tookDamage)
            {
                return;
            }

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
