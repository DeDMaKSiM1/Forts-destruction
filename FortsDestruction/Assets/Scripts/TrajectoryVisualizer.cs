using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class TrajectoryVisualizer : MonoBehaviour
    {
        //Ограничение угла поворота пушки Y координаты
        [SerializeField] private float AngleLimitationY;
        //
        [SerializeField] private float ProjectileSpeed = 2f;
        //Для ручной регулировки силы выстрела занулить переменную ниже
        [SerializeField] private float minDistance = maxDistance;
        //Для гибкости в начале расчета позиции траектории
        [SerializeField] private Transform initialPosition;

        private float distance;
        private bool isDragging;
        private Vector2 mousePosition;
        private Vector2 launchDirection;
        private LineRenderer trajectoryLine;

        //Ограничение угла поворота пушки X координаты
        private const float angleLimitationX = 0;
        //Ограничение длины траектории
        private const float maxDistance = 8.5f;

        private void Start()
        {
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
            float angle = Mathf.Atan2(launchDirection.y, launchDirection.x) * Mathf.Rad2Deg;
            //Проверка и ограничение угла
            if (angle > 90f)
                angle = 90f;
            else if (angle < -5f)
                angle = -5f;
            //Преобразование угла обратно в радианы и создание нового вектора направления
            launchDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

            Debug.Log(angle);
            //Устанавливаем количество точек на линии
            int segments = 20;
            //Прибавляем 1 чтобы включить конечную точку линии
            trajectoryLine.positionCount = (segments / 2) + 1;

            //Чтобы не объвлять каждый раз переменную в цикле
            float t;
            //Цикл для установки точек в LineRenderer, вычисление позиций точек
            for (int i = 0; i <= segments; i++)
            {
                //Вычисляем нормализованное значение от 0 до 1. Нужно для интерполяции между начальной
                //и конечной точками траектории
                if (i % 2 == 0)
                {
                    t = i / (float)segments;
                    Vector3 point = CalcTrajectoryPoint(t, launchDirection, distance);
                    trajectoryLine.SetPosition(i / 2, point);
                }
            }
        }
        private Vector3 CalcTrajectoryPoint(float t, Vector3 launchDirection, float distance)
        {
            //Получаем значение гравитации
            float gravity = Physics2D.gravity.y;
            //Задаем линейное движение (здесь можно умножить на какое-то значение, чтобы получить
            //различное поведение траекторий
            Vector3 point = initialPosition.position + launchDirection * distance * t * ProjectileSpeed;
            //Добавляем влияние гравитации
            point.y += gravity * t * t;
            return point;
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
        }


    }
}
