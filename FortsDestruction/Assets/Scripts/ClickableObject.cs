
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts
{
    public class ClickableObject : MonoBehaviour
    {
        public UnityEvent MouseUp;

        protected void OnMouseUp()
        {
            MouseUp.Invoke();
        }
    }
}
