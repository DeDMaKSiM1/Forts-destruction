using UnityEngine;

namespace Assets.Scripts.Projectile
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private Transform SpawnPosition;
        [SerializeField] private GameObject ProjectilePrefab;
        [SerializeField] private float launchForce = 100f;
        private bool isDragging;
        private ProjectilePhysics projectile;

        public Vector2 LaunchDirection { get; private set; }
        public float LaunchForce { get => launchForce; }
        //////
        private CursorDistanceTracker cursorTracker;
        private void Start()
        {
            cursorTracker = GetComponent<CursorDistanceTracker>();
        }
        private void Update()
        {
            if (isDragging)
                LaunchDirection = cursorTracker.CalculateDistanceToObject();
        }

        private void Launch()
        {
            //Спавн снаряда
            GameObject projectileObject = Instantiate(ProjectilePrefab, SpawnPosition.position, Quaternion.identity);
            //Создание ссылки на скрипт физики снаряда
            projectile = projectileObject.GetComponent<ProjectilePhysics>();
            //Инициализация начальных значений для расчета полета снаряда
            projectile.InitializationProjectile(LaunchDirection.normalized, launchForce);
        }


        private void OnMouseDown()
        {
            isDragging = true;
        }
        private void OnMouseUp()
        {
            isDragging = false;
            Launch();
        }
    }
}
