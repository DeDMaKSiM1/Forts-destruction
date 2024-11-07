using Assets.Scripts.Physics;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ProjectileLauncher  : MonoBehaviour
    {
        [SerializeField] private GameObject ProjectilePrefab;
        [SerializeField] private float LaunchForce = 10f;
        [SerializeField] private Transform SpawnPosition;


        [ContextMenu("Запустить снаряд")]
        public void LaunchProjectile()
        {
            GameObject projectile = Instantiate(ProjectilePrefab, SpawnPosition);
            if (!projectile.TryGetComponent<ProjectilePhysics>(out ProjectilePhysics projectilePhysics))
            {
                Debug.Log("Не смог через TryGetComponent найти ProjectilePhysics");
                return;
            }
            projectilePhysics.SetLaunchForce(LaunchForce);

            projectilePhysics.Launch();
        }
    }
}
