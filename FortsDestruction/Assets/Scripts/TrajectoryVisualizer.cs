using UnityEngine;

namespace Assets.Scripts
{
    public class TrajectoryVisualizer : MonoBehaviour
    {
        //Ограничение угла поворота пушки X координаты
        [SerializeField] private float AngleLimitationPositive = 90f;
        //Ограничение угла поворота пушки Y координаты
        [SerializeField] private float AngleLimitationNegative = -15f;
        //
        [SerializeField] private float ProjectileSpeed = 2f;
        //Для ручной регулировки силы выстрела занулить переменную ниже
        [SerializeField] private float minDistance = maxDistance;
        //Для гибкости в начале расчета позиции траектории
        [SerializeField] private Transform initialPosition;

        private float gravity;
        private float angle;
        private float normallizedTimeValue;
        private float distance;
        private bool isDragging;
        private Vector2 launchDirection;
        private Vector2 mousePosition;
        private LineRenderer trajectoryLine;
        private ProjectileLauncher projectileLauncher;

        //Устанавливаем количество точек на линии
        private const int Segments = 20;
        //Ограничение длины траектории
        private const float maxDistance = 8.5f;

        private void Start()
        {
            trajectoryLine = GetComponent<LineRenderer>();
            projectileLauncher = GetComponent<ProjectileLauncher>();
            trajectoryLine.enabled = false;
        }
        private void Update()
        {
            if (isDragging)
            {
                UpdateTrajectory();
            }
        }

        private void UpdateTrajectory()
        {
            //Рассчитывает координаты мыши через местоположение мыши на экране 
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Рассчитывает направление запуска объекта
            launchDirection = new Vector2(initialPosition.position.x - mousePosition.x, initialPosition.position.y - mousePosition.y);

            //Рассчитываем силу запуска, если значение < min, то => min, если > max, то => max            
            distance = Mathf.Clamp(launchDirection.magnitude, minDistance, maxDistance);
            //Нормализуем вектор чтобы получить направление вектора в единичном виде
            launchDirection.Normalize();

            //Ограничение угла
            //Угол в градусах
            angle = Mathf.Atan2(launchDirection.y, launchDirection.x) * Mathf.Rad2Deg;
            //Проверка и ограничение угла
            if (angle > AngleLimitationPositive)
                angle = AngleLimitationPositive;
            else if (angle < AngleLimitationNegative)
                angle = AngleLimitationNegative;

            //Преобразование угла обратно в радианы и создание нового вектора направления
            launchDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;


            //Прибавляем 1 чтобы включить конечную точку линии
            trajectoryLine.positionCount = (Segments / 2) + 1;


            //Цикл для установки точек в LineRenderer, вычисление позиций точек
            for (int i = 0; i <= Segments; i++)
            {
                //Вычисляем нормализованное значение от 0 до 1. Нужно для интерполяции между начальной
                //и конечной точками траектории
                if (i % 2 == 0)
                {
                    normallizedTimeValue = i / (float)Segments;
                    Vector3 point = CalcTrajectoryPoint();
                    trajectoryLine.SetPosition(i / 2, point);
                }
            }
        }
        private Vector3 CalcTrajectoryPoint()
        {
            //Получаем значение гравитации
            gravity = Physics2D.gravity.y;
            //Задаем линейное движение (здесь можно умножить на какое-то значение, чтобы получить
            //различное поведение траекторий
            Vector3 point = initialPosition.position + (Vector3)launchDirection * distance * normallizedTimeValue;
            //Добавляем влияние гравитации
            point.y += gravity * normallizedTimeValue * normallizedTimeValue;
            Debug.Log(point);

            return point;
        }
        private void LaunchProjectile()
        {
            projectileLauncher.Launch(launchDirection, distance);
        }
        private void OnMouseDown()
        {
            isDragging = true;
            trajectoryLine.enabled = true;
        }
        private void OnMouseUp()
        {
            isDragging = false;
            trajectoryLine.enabled = false;
            LaunchProjectile();
        }


    }
}
