using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts
{
    public class ClickableObject : MonoBehaviour
    {
        public UnityEvent MouseDown;
        public UnityEvent MouseUp;

        private void OnMouseUp()
        {
            MouseUp.Invoke();
        }
        private void OnMouseDown()
        {
            MouseDown.Invoke();
        }
    }
}
