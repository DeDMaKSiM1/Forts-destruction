using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class BlockPhysics : MonoBehaviour
    {
        private Rigidbody2D rb;

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
            rb.AddForce(new(-0.5f * Random.Range(2f, 6f), 0.7f * Random.Range(-4f, 6f)));
        }
        public void AddForceHeroSide()
        {
            rb.AddForce(new(0.5f * Random.Range(2f, 6f), 0.7f * Random.Range(-4f, 6f)));
        }
    }
}
