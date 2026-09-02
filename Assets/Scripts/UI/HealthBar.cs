using Tutorial.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial.Presentation
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Slider))]
    public sealed class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _health;

        private Slider _slider;
        private bool _hasStarted;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            if (_health == null)
            {
                return;
            }

            _health.HealthChanged += Refresh;

            if (_hasStarted)
            {
                Refresh(_health.CurrentHealth, _health.MaxHealth);
            }
        }

        private void Start()
        {
            _hasStarted = true;

            if (_health == null)
            {
                Debug.LogError($"{nameof(HealthBar)} on {name} requires a Health reference.", this);
                return;
            }

            Refresh(_health.CurrentHealth, _health.MaxHealth);
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.HealthChanged -= Refresh;
            }
        }

        private void Refresh(int currentHealth, int maxHealth)
        {
            _slider.minValue = 0f;
            _slider.maxValue = maxHealth;
            _slider.value = currentHealth;
        }
    }
}
