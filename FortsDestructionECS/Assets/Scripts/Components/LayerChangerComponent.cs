using UnityEngine;

namespace Assets.Scripts.Components
{
    public class LayerChangerComponent: MonoBehaviour
    {
        [SerializeField] private string LayerName;

        public void ToChangeLayer()
        {
            int layer = LayerMask.NameToLayer(LayerName);
            if(layer == -1)
            {
                Debug.Log($"Layer {LayerName} не найден!");
                return;
            }
            gameObject.layer = layer;
        }
    }
}
