using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public float Health { get; private set; } = 100f;
    public UnityEvent OnHealthZero;

    public void ModifyHealth(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnHealthZero.Invoke();
        }
    }
}
