using UnityEngine;

namespace Assets.Scripts.Projectile
{
    public class LaunchDirectionCalculator : MonoBehaviour
    {
        [SerializeField] private Transform InitialPosition;
        //Ограничение угла поворота пушки Y координаты
        [SerializeField] private float angleLimitationYPositive = 90f;
        [SerializeField] private float angleLimitationYNegative = -5f;

        private Vector2 mousePosition;
        private Vector2 launchDirection;
        public Vector2 DirectionCalc()
        {
            //Рассчитывает координаты мыши через местоположение мыши на экране 
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Рассчитывает направление запуска объекта
            launchDirection = new Vector2(InitialPosition.position.x - mousePosition.x, InitialPosition.position.y - mousePosition.y);

            //Нормализуем вектор чтобы получить направление вектора в единичном виде
            launchDirection.Normalize();

            //Ограничение угла
            //Угол в градусах
            float angle = Mathf.Atan2(launchDirection.y, launchDirection.x) * Mathf.Rad2Deg;
            //Проверка и ограничение угла
            if (angle > angleLimitationYPositive)
                angle = angleLimitationYPositive;
            else if (angle < angleLimitationYNegative)
                angle = angleLimitationYNegative;
            //Преобразование угла обратно в радианы и создание нового вектора направления
            launchDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

            return launchDirection;
        }
    }
}
