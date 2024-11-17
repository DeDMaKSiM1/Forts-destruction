

using Assets.Scripts.ComponentsAndTags.Block;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.ComponentsAndTags.Projectile
{
    public readonly partial  struct ProjectileAspect : IAspect
    {
        //Ссылка на сущность
        public readonly Entity Entity;

        //Ссылка на компонент ProjectileProperties
        private readonly RefRO<ProjectileProperties> _projectileProperties;

        //Свойство для получения префаба снаряда
        public Entity ProjectilePrefab => _projectileProperties.ValueRO.ProjectilePrefab;

 
        

        //Метод, возвращающий Transform для снаряда
        public LocalTransform GetProjectileTransform()
        {
            //Создает и возвращает новый Transform  Position в месте нажатия мышки, стандартным масштабом и поворотом
            return new LocalTransform
            {
                Position = GetMousePosition(),
                Rotation = quaternion.identity,
                Scale = 1f
            };
        }





        private float3 GetMousePosition()
        {
            var screenPosition = Input.mousePosition;
            var worldPosition = Camera.main.ScreenToWorldPoint(new float3(screenPosition.x, screenPosition.y, 10f));
            return worldPosition;
        }
    }
}
