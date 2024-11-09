using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class ProjectilePhysics : MonoBehaviour
    {
        //Для гибкости в начале расчета позиции траектории
        [SerializeField] private Transform initialPosition;

        private float gravity;
        private Vector2 launchDirection;
        private Rigidbody2D rb;
        private bool isFlying;
        private float distance;
        private float timeElapsed;
        private float flightDuration;
        private float normallizedTimeValue;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        //public void Launch(Vector2 moveVector, float Force)
        //{
        //    Debug.Log($"moveVector * Force {moveVector * Force}");
        //    rb.AddForce(moveVector * Force);
        //}
        public void Initialize(Vector2 direction, float distanceToTravel)
        {
            distance = distanceToTravel;
            launchDirection = direction;
            flightDuration = distance / 5f;
            isFlying = true;
        }
        private void Update()
        {
            if (isFlying)
            {
                UpdateTrajectory();
            }
        }
        private void UpdateTrajectory()
        {
            timeElapsed += Time.deltaTime;

            //Вычисляем позицию снаряда
            normallizedTimeValue = timeElapsed / flightDuration;
            if (normallizedTimeValue > 1)
            {
                //Ограничение времени, чтобы не выходить за пределы
                normallizedTimeValue = 1;
                isFlying = false;
            }
            Vector3 newPosition = CalcTrajectoryPoint();
            //rb.linearVelocity = velocity;

            ////Ограничение угла
            ////Угол в градусах
            //float angle = Mathf.Atan2(LaunchDirection.y, LaunchDirection.x) * Mathf.Rad2Deg;
            ////Проверка и ограничение угла
            //if (angle > AngleLimitationPositive)
            //    angle = AngleLimitationPositive;
            //else if (angle < AngleLimitationNegative)
            //    angle = AngleLimitationNegative;

            ////Преобразование угла обратно в радианы и создание нового вектора направления
            //LaunchDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;                        
        }
        private Vector3 CalcTrajectoryPoint()
        {
            //Получаем значение гравитации
            gravity = Physics2D.gravity.y;
            //Задаем линейное движение (здесь можно умножить на какое-то значение, чтобы получить
            //различное поведение траекторий
            Vector3 point = transform.position + (Vector3)launchDirection * distance * normallizedTimeValue /** ProjectileSpeed*/;
            //Добавляем влияние гравитации
            point.y += gravity * normallizedTimeValue * normallizedTimeValue;
            Debug.Log(point);

            return point;
        }
    }
}
