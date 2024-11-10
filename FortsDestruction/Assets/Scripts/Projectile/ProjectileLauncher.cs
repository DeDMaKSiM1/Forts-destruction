using UnityEngine;

namespace Assets.Scripts.Projectile
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private Transform SpawnPosition;
        [SerializeField] private GameObject ProjectilePrefab;
        [SerializeField] private float launchForce = 100f;
        private ProjectilePhysics projectile;
        private ClickableObject clickable;

        public Vector2 LaunchDirection { get; private set; }
        public float LaunchForce { get => launchForce; }
        //////
        private CursorDistanceTracker cursorTracker;
        private void Awake()
        {
            cursorTracker = GetComponent<CursorDistanceTracker>();
            clickable = GetComponent<ClickableObject>();

        }
        private void Start()
        {
        }
        private void Update()
        {            
            if (clickable.IsDragging)
                LaunchDirection = cursorTracker.CalculateDistanceToObject();
        }

        public void Launch()
        {
            //Спавн снаряда
            GameObject projectileObject = Instantiate(ProjectilePrefab, SpawnPosition.position, Quaternion.identity);
            //Создание ссылки на скрипт физики снаряда
            projectile = projectileObject.GetComponent<ProjectilePhysics>();
            //Инициализация начальных значений для расчета полета снаряда
            projectile.InitializationProjectile(LaunchDirection.normalized, launchForce);
        }


        //private void OnMouseDown()
        //{
        //    isDragging = true;
        //}
        //private void OnMouseUp()
        //{
        //    isDragging = false;
        //    Launch();
        //}
    }
}
