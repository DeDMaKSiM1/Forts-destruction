using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts
{
    public class ClickableObject : MonoBehaviour
    {
        public UnityEvent MouseDown;
        public UnityEvent MouseUp;
        
        public bool IsDragging { get; private set; }

        private void OnMouseUp()
        {
            IsDragging = false;
            MouseUp.Invoke();
        }
        private void OnMouseDown()
        {
            IsDragging = true;
            MouseDown.Invoke();
        }
    }
}
