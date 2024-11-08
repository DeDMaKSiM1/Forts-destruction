

using System;
using UnityEngine;

namespace Assets.Scripts.ScriptStorage
{
    public class OnStretchableObject : ClickableObject
    {
        private float maxStretchForce = 5f;
        private bool IsDragging;
        private Vector2 originalScale;
        private Vector2 initialMousePosition;

        private void Start()
        {
            originalScale = transform.localScale;
        }
        private void FixedUpdate()
        {
            //Отслеживаем состояние объекта(нажат или нет) и растягиваем если нажат
            if (IsDragging)
            {
                Drag();
            }
        }

        private void Drag()
        {
            //Получаем вектор текущей позиции мыши
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Вычисляем разницу вектора текущего положения мыши и изначального
            Vector2 dragDirection = currentMousePosition - initialMousePosition;
            float stretchForce = dragDirection.magnitude;

            //Ограничиваем максимальное растяжение
            if (stretchForce > maxStretchForce)
                stretchForce = maxStretchForce;

            transform.localScale = new Vector2(originalScale.x + dragDirection.x, originalScale.y + dragDirection.y);
        }

        private void OnMouseDown()
        {
            //Задаем изначальный вектор позции мыши при нажатии на объект мышью
            initialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Задаем булевский флаг, который будет в FixedUpdate контролировать растяжение объекта
            IsDragging = true;
        }
        private new void OnMouseUp()
        {
            IsDragging = false;
            transform.localScale = originalScale;
            base.OnMouseUp();
        }



    }
}
