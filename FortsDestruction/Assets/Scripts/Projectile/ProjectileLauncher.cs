using UnityEngine;

namespace Assets.Scripts.Projectile
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private GameObject ProjectilePrefab;
        [SerializeField] private float launchForce = 100f;

        private Vector2 launchDirection;
        private CursorDistanceTracker cursorTracker;
        private ProjectilePhysics projectile;

        public float LaunchForce { get => launchForce; }
        public Transform SpawnPosition { get => spawnPosition; }
        public ProjectilePhysics ProjectilePhysics { get => projectile; }
        private void Start()
        {
            cursorTracker = GetComponent<CursorDistanceTracker>();
            projectile = ProjectilePrefab.GetComponent<ProjectilePhysics>();
        }
        private void Update()
        {
            if (ClickableObject.IsDragging)
                launchDirection = cursorTracker.CalculateDistanceToObject();
        }
        public void Launch()
        {
            //Спавн снаряда
            GameObject projectileObject = Instantiate(ProjectilePrefab, SpawnPosition.position, Quaternion.identity);
            //Создание ссылки на скрипт физики снаряда
            projectile = projectileObject.GetComponent<ProjectilePhysics>();
            //Инициализация начальных значений для расчета полета снаряда
            projectile.InitializationProjectile(launchDirection.normalized, launchForce);
        }
    }
}
