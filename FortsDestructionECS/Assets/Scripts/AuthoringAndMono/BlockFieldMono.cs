using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;
using Assets.Scripts.ComponentsAndTags;

namespace Assets.Scripts
{
    //Класс MonoBehavior для настройки поля блоков в Unity
    public class BlockFieldMono : MonoBehaviour
    {
        //Размер поля, на котором будут спавниться блоки
        public float2 BlockFieldSize;
        //Количество блоков для создания
        public int NumberOfBlocksToSpawn;
        //Префаб блока, который будет использоваться для создания сущностей
        public GameObject BlockPrefab;
        //Начальное значение для генератора случаных чисел
        public uint RandomSeed;
    }
    //Класс, который преобразовывает BlockFieldMono в ECS-компоненты
    public class BlockFieldBaker : Baker<BlockFieldMono>
    {
        //Метод для создания ECS-компонентов
        public override void Bake(BlockFieldMono authoring)
        {
            //Получает сущность для компонента
            var blockFieldEntity = GetEntity(TransformUsageFlags.Dynamic);
            //Добавляет компонент BlockFieldProperies к сущности, инициализируя его данными из BlockFieldMono
            AddComponent(blockFieldEntity, new BlockFieldProperties
            {
                BlockFieldSize = authoring.BlockFieldSize,
                NumberOfBLocksToSpawn = authoring.NumberOfBlocksToSpawn,
                BlockPrefab = GetEntity(authoring.BlockPrefab, TransformUsageFlags.Dynamic)
            });
            //Добавляет компонент BlockFieldRandom к сущности, инициализируя генератор случайных чисел
            AddComponent(blockFieldEntity, new BlockFieldRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
        }
    }
}
