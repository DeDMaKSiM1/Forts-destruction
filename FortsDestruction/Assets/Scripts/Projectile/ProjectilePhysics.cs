using UnityEngine;

namespace Assets.Scripts.Projectile
{
    public class ProjectilePhysics : MonoBehaviour
    {
        [SerializeField] private float mass = 1f;

        private Rigidbody2D rb;
        private float initialVelocity;
        public float Mass { get => mass; }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void InitializationProjectile(Vector2 launchDirection, float launchForce)
        {
            //Вычисление начальной скорости, юнит/с
            initialVelocity = launchForce / mass;
            //Вычисление скорости с направлением
            Vector2 velocity = launchDirection * initialVelocity;
            //Придача скорости физической составляющей снаряда RigidBody2D
            rb.linearVelocity = velocity;

        }
    }
}
