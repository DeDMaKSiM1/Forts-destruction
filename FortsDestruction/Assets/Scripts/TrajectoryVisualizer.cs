using Assets.Scripts.Physics;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class TrajectoryVisualizer : MonoBehaviour
    {
        [SerializeField] private ProjectilePhysicsV2 projectilePhysics;

        private Vector2 startPosition;
        private ProjectileLauncherV2 projectileLauncher;
        private LineRenderer lineRenderer;
        private float normallizedTime;
        private bool isDragged;
        private const int segments = 30;

        private void Start()
        {
            projectileLauncher = GetComponent<ProjectileLauncherV2>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
            startPosition = transform.position;
        }
        private void Update()
        {
            if (isDragged)
                DrawTrajectory();
        }
        private void DrawTrajectory()
        {
            //Устанавливаем количество точек
            lineRenderer.positionCount = segments + 1;

            //Debug.Log($"launchDirection = {launchDirection}");
            float initialVelocity = projectileLauncher.LaunchForce / projectilePhysics.Mass;

            Vector2 velocity = projectileLauncher.LaunchDirection * initialVelocity;
            //Расчет позиции для каждой точки
            for (int i = 0; i <= segments; i++)
            {
                normallizedTime = i / segments;
                float x = startPosition.x + velocity.x * normallizedTime;
                float y = startPosition.y + velocity.y * normallizedTime - 0.5f * Physics2D.gravity.y * normallizedTime * normallizedTime;
                lineRenderer.SetPosition(i, new Vector2(x, y));
            }
        }
        private void OnMouseDown()
        {
            isDragged = true;
            lineRenderer.enabled = true;
            DrawTrajectory();
        }
        private void OnMouseUp()
        {
            isDragged = false;
            lineRenderer.enabled = false;
        }
    }
}
