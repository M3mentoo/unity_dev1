using UnityEngine;

namespace Tutorial.Combat
{
    public readonly struct DamageInfo
    {
        public int Amount { get; }
        public Vector2 Direction { get; }

        public DamageInfo(int amount, Vector2 direction)
        {
            Amount = amount;
            Direction = direction.sqrMagnitude > 0f
                ? direction.normalized
                : Vector2.zero;
        }
    }
}
