using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Projectile
{
    public class TrajectoryVisualizer : MonoBehaviour
    {
        private float distance;
        private Vector2 launchDirection;
        private LaunchDirectionCalculator cursorTracker;
        private LineRenderer trajectoryLine;
        private ProjectileLauncher launcher;

        private void Start()
        {
            trajectoryLine = GetComponent<LineRenderer>();
            trajectoryLine.enabled = false;
            cursorTracker = GetComponent<LaunchDirectionCalculator>();
            launcher = GetComponent<ProjectileLauncher>();
        }
        private void Update()
        {
            if (ClickableComponent.IsDragging)
            {
                UpdateTrajectory();
            }
        }

        private void UpdateTrajectory()
        {
            //Получение расстояния между курсором и нажатым объектом
            launchDirection = cursorTracker.DirectionCalc();

            //Вычисление начальной скорости
            distance = launcher.LaunchForce / launcher.ProjectilePhysics.Mass;

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
            point.x = launcher.SpawnPosition.position.x + launchDirection.x * distance * t;
            //Добавляем влияние гравитации
            point.y = launcher.SpawnPosition.position.y + launchDirection.y * distance * t + 0.5f * gravity * t * t;
            point.z = 0;
            return point;
        }




    }
}
