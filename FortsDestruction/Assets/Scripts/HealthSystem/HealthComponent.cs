using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.HealthSystem
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health;
        public float Health
        {
            get => health;
            private set => health = value;
        }
        public UnityEvent OnHealthZero;

        public void CheckHealth()
        {
            if (Health <= 0)
            {
                OnHealthZero.Invoke();
            }
        }

        public void TakeDamage(float damage)
        {
            Health += damage;
            CheckHealth();
        }
    }
}

