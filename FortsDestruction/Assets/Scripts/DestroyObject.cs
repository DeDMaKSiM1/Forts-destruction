using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
    public void OnDelayDestroy()
    {
        Destroy(gameObject, 1f);
    }
}
