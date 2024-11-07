using System;
using UnityEngine;


namespace Assets.Scripts.HealthSystem
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private float ModifyValue = -10;
        [SerializeField] private string Tag;
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Обнаружен коллайдер");
            if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable) && other.gameObject.CompareTag(Tag))
            {
                Debug.Log("Обнаружен нужный коллайдер");

                damageable.TakeDamage(ModifyValue);
            }
        }
    }
}
