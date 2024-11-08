using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class TrajectoryVisualizer : MonoBehaviour
    {
        [SerializeField] private float ProjectileSpeed = 2f;
        //Для ручной регулировки силы выстрела занулить переменную ниже
        [SerializeField] private float minDistance = maxDistance;

        private float distance;        
        private bool IsDragging;
        private Vector2 mousePosition;
        private Vector2 launchDirection;
        private LineRenderer trajectoryLine;

        private const float maxDistance = 8.5f;

        private void Start()
        {
            trajectoryLine = GetComponent<LineRenderer>();
            trajectoryLine.enabled = false;
        }
        private void Update()
        {
            if (IsDragging)
            {
                UpdateTrajectory();
            }
        }

        private void UpdateTrajectory()
        {
            //Рассчитывает координаты мыши через местоположение мыши на экране 
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Рассчитывает направление запуска объекта
            launchDirection = new Vector2(transform.position.x - mousePosition.x, transform.position.y - mousePosition.y);
            //Рассчитываем силу запуска, если значение < min, то => min, если > max, то => max
            //
            distance = Mathf.Clamp(launchDirection.magnitude, minDistance, maxDistance);

            //Нормализуем вектор чтобы получить направление вектора в единичном виде
            launchDirection.Normalize();

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
            Vector3 point = transform.position + launchDirection * distance * t * ProjectileSpeed;
            //Добавляем влияние гравитации
            point.y += gravity * t * t;
            return point;
        }
        private void OnMouseDown()
        {
            IsDragging = true;
            trajectoryLine.enabled = true;
        }
        private void OnMouseUp()
        {
            IsDragging = false;
            trajectoryLine.enabled = false;
        }


    }
}
