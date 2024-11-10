
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class CursorDistanceTracker : MonoBehaviour
    {
        [SerializeField] private Transform initialPosition;
        private Vector2 mousePosition;
        private Vector2 launchDirection;


        public Vector2 CalculateDistanceToObject()
        {
            //Рассчитывает координаты мыши через местоположение мыши на экране 
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Рассчитывает направление запуска объекта
            launchDirection = new Vector2(initialPosition.position.x - mousePosition.x, initialPosition.position.y - mousePosition.y);

            //Нормализуем вектор чтобы получить направление вектора в единичном виде
            launchDirection.Normalize();
            return launchDirection;
        }

    }
}
