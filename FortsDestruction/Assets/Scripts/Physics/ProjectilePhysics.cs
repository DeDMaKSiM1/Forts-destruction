using System;
using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class ProjectilePhysics : MonoBehaviour
    {
        private float projectileForce;
        private Vector2 moveVector = new Vector2(2.5f, 5f);
        private Rigidbody2D rb;


        public void SetLaunchForce(float Force)
        {
            Initialize();

            projectileForce = Force;
            //Debug.Log("Сила снаряда установлена");
        }
        public void Launch()
        {
            if(rb == null)
            {
                //Debug.LogError($"rb == null у объекта {gameObject.name}");
                return ;
            }
            rb.AddForce(moveVector * projectileForce);
            //Debug.Log($"Снаряд запущен с силой {projectileForce}");
        }
        private void Initialize()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                //Debug.LogError($"RigidBody не найден на объекте {gameObject.name}");
                return;
            }
            if (rb != null)
            {
                //Debug.Log($"RigidBody успешно инициализирован на объекте {gameObject.name}");
            }
        }
    }
}
