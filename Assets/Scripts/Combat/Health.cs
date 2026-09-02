using System;
using UnityEngine;

namespace Tutorial.Combat
{
    [DisallowMultipleComponent]
    public sealed class Health : MonoBehaviour
    {
        public event Action<int, int> HealthChanged;
        public event Action<DamageInfo> Damaged;
        public event Action Died;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0;
        public bool IsInvulnerable => Time.time < _invulnerableUntil;

        [SerializeField, Min(1)] private int _maxHealth = 5;
        [SerializeField, Min(0f)] private float _invulnerabilityDuration = 0.75f;

        private float _invulnerableUntil;

        private void Awake()
        {
            CurrentHealth = _maxHealth;
        }

        public bool TryTakeDamage(int amount)
        {
            return TryTakeDamage(new DamageInfo(amount, Vector2.zero));
        }

        public bool TryTakeDamage(DamageInfo damage)
        {
            if (damage.Amount <= 0 || IsDead || IsInvulnerable)
            {
                return false;
            }

            CurrentHealth = Mathf.Max(0, CurrentHealth - damage.Amount);
            _invulnerableUntil = Time.time + _invulnerabilityDuration;
            HealthChanged?.Invoke(CurrentHealth, _maxHealth);
            Damaged?.Invoke(damage);

            Debug.Log(
                $"{name} health: {CurrentHealth}/{_maxHealth}",
                this);

            if (IsDead)
            {
                Died?.Invoke();
            }

            return true;
        }

        public bool TryHeal(int amount)
        {
            if (amount <= 0 || IsDead || CurrentHealth >= _maxHealth)
            {
                return false;
            }

            CurrentHealth = Mathf.Min(_maxHealth, CurrentHealth + amount);
            HealthChanged?.Invoke(CurrentHealth, _maxHealth);
            return true;
        }

        public void RestoreFullHealth()
        {
            CurrentHealth = _maxHealth;
            _invulnerableUntil = 0f;
            HealthChanged?.Invoke(CurrentHealth, _maxHealth);
        }
    }
}
