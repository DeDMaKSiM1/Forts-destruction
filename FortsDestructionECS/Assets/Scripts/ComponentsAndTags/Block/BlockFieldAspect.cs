

using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.ComponentsAndTags.Block
{
    //Определяет аспект, который упрощает доступ к данным ECS
    public readonly partial struct BlockFieldAspect : IAspect
    {
        //Ссылка на сущность
        public readonly Entity Entity;
        //Ссылка на компонент Transform
        private readonly RefRW<LocalTransform> _transform;
        //Свойство для доступа к компоненту Transform
        private LocalTransform Transform => _transform.ValueRO;

        //Ссылка на компонент BlockProperties
        private readonly RefRO<BlockFieldProperties> _blockProperties;
        //Ссылка на компонент BlockFieldRandom
        private readonly RefRW<BlockFieldRandom> _blockFieldRandom;

        //Свойство для получения количество блоков
        public int NumberOfBlocksToSpawn => _blockProperties.ValueRO.NumberOfBLocksToSpawn;
        //Свойство для получения префаба блока
        public Entity BlockPrefab => _blockProperties.ValueRO.BlockPrefab;

        //Метод, возвращающий случайный Transform для блока
        public LocalTransform GetRandomBlockTransform()
        {
            //Создает и возвращает новый Transform со случайным Position, стандартным масштабом и поворотом
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = quaternion.identity,
                Scale = 1f
            };
        }
        //Метод, генерирующий случайную позицию в пределах поля блоков
        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            //Цикл, повторяющий генерацию случайной позиции, пока она находится в "Нейтральной зоне"
            do
            {
                //Генерирует случайную позицию в пределах заданных границ
                randomPosition = _blockFieldRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            }
            while (math.distancesq(Transform.Position, randomPosition) <= NEUTRAL_ZONE_RADIUS);

            return randomPosition;
        }
        //Вычисление минимальных координат угла поля
        private float3 MinCorner => Transform.Position - HalfDimensions;
        //Вычисление максимальных координат угла поля
        private float3 MaxCorner => Transform.Position + HalfDimensions;
        //Вычисление половины размера поля блоков
        private float3 HalfDimensions => new()
        {
            x = _blockProperties.ValueRO.BlockFieldSize.x,
            y = _blockProperties.ValueRO.BlockFieldSize.y,
            z = 0f
        };
        //Константа, определяющая радиус "Нейтральной зоны" вокруг текущей позиции, в которой не спавнятся блоки
        private const float NEUTRAL_ZONE_RADIUS = 20;
    }
}
