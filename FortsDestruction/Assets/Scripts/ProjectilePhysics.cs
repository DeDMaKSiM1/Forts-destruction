using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ProjectilePhysics : MonoBehaviour
    {
        [SerializeField] private float Force = 10f;

        private Vector2 moveVector = new Vector2(2.5f, 5f);
        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        [ContextMenu("Запустить снаряд")]
        public void LaunchProjectile()
        {
            rb.AddForce(moveVector * Force);
        }
    }
}
