using System;
using UnityEngine;

namespace Assets.Scripts.HealthSystem
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private float ModifyValue = -10;
        [SerializeField] private string Tag;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable damageable) && collision.CompareTag(Tag))
            {
                damageable.TakeDamage(ModifyValue);
            }
        }
    }
}
