using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts
{
    public class ClickableObject : MonoBehaviour
    {
        public UnityEvent MouseDown;
        public UnityEvent MouseUp;
        public static bool IsDragging { get; private set; }
        private void OnMouseDown()
        {
            IsDragging = true;
            MouseDown.Invoke();
        }
        private void OnMouseUp()
        {
            IsDragging = false;
            MouseUp.Invoke();
        }
    }
}
