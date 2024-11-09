using UnityEngine;
using Assets.Scripts.Physics;

namespace Assets.Scripts
{
    public class ProjectileLauncherV2 :MonoBehaviour
    {
        [SerializeField] private Transform SpawnPosition;
        [SerializeField] private GameObject ProjectilePrefab;
        [SerializeField] private float launchForce = 100f;
        private bool isDragging;
        private Vector2 launchDirection;
        private ProjectilePhysicsV2 projectile;
        private void Update()
        {
            if (isDragging)
                ToDirectProjectile();
        }
        private void ToDirectProjectile()
        {
            //Рассчитывает координаты мыши через местоположение мыши на экране 
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Рассчитывает направление запуска объекта
            launchDirection = new Vector2(SpawnPosition.position.x - mousePosition.x, SpawnPosition.position.y - mousePosition.y);

            //Нормализуем вектор чтобы получить направление вектора в единичном виде
            launchDirection.Normalize();
            //Debug.Log($"launchDirection = {launchDirection}");
        }
        private void Launch()
        {
            GameObject projectileObject = Instantiate(ProjectilePrefab, SpawnPosition.position,Quaternion.identity);
            projectile = projectileObject.GetComponent<ProjectilePhysicsV2>();
            projectile.InitializationProjectile(launchDirection, launchForce);
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
