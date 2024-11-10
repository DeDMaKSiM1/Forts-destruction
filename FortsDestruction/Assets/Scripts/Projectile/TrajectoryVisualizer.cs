using UnityEngine;

namespace Assets.Scripts.Projectile
{
    public class TrajectoryVisualizer : MonoBehaviour
    {
        [SerializeField] private float launchForce = 10f;
        [SerializeField] private float mass = 1f;

        //Ограничение угла поворота пушки Y координаты
        [SerializeField] private float AngleLimitationYPositive = 90f;
        [SerializeField] private float AngleLimitationYNegative = -5f;

        //Для гибкости в начале расчета позиции траектории
        [SerializeField] private Transform initialPosition;

        private CursorDistanceTracker cursorTracker;
        private float distance;
        private bool isDragging;
        private Vector2 launchDirection;
        private LineRenderer trajectoryLine;

        //Ограничение угла поворота пушки X координаты
        private const float angleLimitationX = 0;
        //Ограничение длины траектории
        private const float maxDistance = 10f;

        private void Start()
        {
            cursorTracker = GetComponent<CursorDistanceTracker>();
            trajectoryLine = GetComponent<LineRenderer>();
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
            //Получение расстояния между курсором и нажатым объектом
            launchDirection = cursorTracker.CalculateDistanceToObject();

            //Вычисление начальной скорости
            distance = launchForce / mass;

            //Ограничение угла
            //Угол в градусах
            float angle = Mathf.Atan2(launchDirection.y, launchDirection.x) * Mathf.Rad2Deg;
            //Проверка и ограничение угла
            if (angle > AngleLimitationYPositive)
                angle = AngleLimitationYPositive;
            else if (angle < AngleLimitationYNegative)
                angle = AngleLimitationYNegative;
            //Преобразование угла обратно в радианы и создание нового вектора направления
            launchDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

            //Debug.Log(angle);
            //Устанавливаем количество точек на линии
            int segments = 20;
            //Прибавляем 1 чтобы включить конечную точку линии
            trajectoryLine.positionCount = segments + 1;

            //Чтобы не объвлять каждый раз переменную в цикле
            float t;
            //Цикл для установки точек в LineRenderer, вычисление позиций точек
            for (int i = 0; i <= segments; i++)
            {
                //Вычисляем нормализованное значение от 0 до 1. Нужно для интерполяции между начальной
                //и конечной точками траектории

                t = i / (float)segments;
                Vector3 point = CalcTrajectoryPoint(t, launchDirection, distance);
                trajectoryLine.SetPosition(i, point);

            }
        }
        private Vector3 CalcTrajectoryPoint(float t, Vector3 launchDirection, float distance)
        {
            //Получаем значение гравитации
            float gravity = Physics2D.gravity.y;
            //Задаем линейное движение (здесь можно умножить на какое-то значение, чтобы получить
            //различное поведение траекторий
            Vector3 point /*= initialPosition.position + launchDirection * distance * t * ProjectileSpeed*/;
            //Вычисляем Х
            point.x = initialPosition.position.x + launchDirection.x * distance * t;
            //Добавляем влияние гравитации
            point.y = initialPosition.position.y + launchDirection.y * distance * t + 0.5f * gravity * t * t;
            point.z = 0;
            return point;
        }

        //Выделить из лаунчера и траектории скрипт Clickable Object
        private void OnMouseDown()
        {
            isDragging = true;
            trajectoryLine.enabled = true;
        }
        private void OnMouseUp()
        {
            isDragging = false;
            trajectoryLine.enabled = false;
        }


    }
}
