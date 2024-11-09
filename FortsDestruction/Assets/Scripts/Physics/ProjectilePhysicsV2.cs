using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class ProjectilePhysicsV2 : MonoBehaviour
    {
        [SerializeField] private float mass = 1f;
        private Rigidbody2D rb;

        public float Mass { get => mass; }
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void InitializationProjectile(Vector2 launchDirection, float launchForce)
        {
            float initialVelocity = launchForce / mass;
            Vector2 velocity = launchDirection * initialVelocity;
            Debug.Log(velocity);
            //float vX = initialVelocity * Mathf.Cos(angle * Mathf.Deg2Rad);
            //float vY = initialVelocity * Mathf.Sin(angle * Mathf.Deg2Rad);

            rb.linearVelocity = velocity;
            //Debug.Log($" rb.linearVelocity = {velocity}");
            //rb.linearVelocity = new Vector2(vX * launchDirection.x, vY * launchDirection.y);
            //Debug.Log($"rb.linearVelocity.x = {vX * launchDirection.x}, y = {vY * launchDirection.y}");
        }
    }
}
