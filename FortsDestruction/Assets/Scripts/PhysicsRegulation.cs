using UnityEngine;


namespace Assets.Scripts
{
    public class PhysicsRegulation : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float explosionForce;
        private Vector2 _explosionEnemyDirection = new(-0.5f, 0.5f);
        private Vector2 _explosionHeroDirection = new(0.5f, 0.5f);
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
        public void AddForceEnemySide()
        {
            explosionForce = Random.Range(2f, 6f);
            rb.AddForce(_explosionEnemyDirection * explosionForce);
        }
        public void AddForceHeroSide()
        {
            explosionForce = Random.Range(2f, 6f);
            rb.AddForce(_explosionHeroDirection * explosionForce);
        }
    }
}
