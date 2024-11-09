using Assets.Scripts.Physics;
using UnityEngine;

namespace Assets.Scripts
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private GameObject ProjectilePrefab;
        [SerializeField] private Transform SpawnPosition;

        private ProjectilePhysics projectilePhysics;

        public void Launch(Vector2 launchDirection, float distance)
        {
            GameObject projectile = Instantiate(ProjectilePrefab, SpawnPosition.position, Quaternion.identity);
            projectilePhysics = projectile.GetComponent<ProjectilePhysics>();
            projectilePhysics.Initialize(launchDirection, distance);
        }
    }
}
