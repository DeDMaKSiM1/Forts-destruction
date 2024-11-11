using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class BlockPhysics : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] private int heroEnemyCoefficient = 1;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void PhysicsOn()
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(new(heroEnemyCoefficient * 0.5f * Random.Range(2f, 6f), 0.7f * Random.Range(-4f, 6f)));
        }

    }
}
