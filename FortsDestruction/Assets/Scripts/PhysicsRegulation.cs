using System;
using UnityEngine;


namespace Assets.Scripts
{
    public class PhysicsRegulation : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float explosionForce = 100f;
        private Vector2 explosionDirection = new(-3, -4);
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void KinematicOn()
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        public void KinematicOff()
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        public void AddForce()
        {
            rb.AddForce(explosionDirection * explosionForce);
        }
    }
}
