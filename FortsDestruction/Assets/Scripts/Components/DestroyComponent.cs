using UnityEngine;

namespace Assets.Scripts.Components
{
    public class DestroyComponent : MonoBehaviour
    {
        public void OnDestroy()
        {
            Destroy(gameObject);
        }
        public void OnDelayDestroy()
        {
            Destroy(gameObject, 10f);
        }
    }
}

