

using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.ComponentsAndTags
{
    public readonly partial struct BlockFieldAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private LocalTransform Transform => _transform.ValueRO;

        private readonly RefRO<BlockFieldProperties> _blockProperties;
        private readonly RefRW<BlockFieldRandom> _blockFieldRandom;

        public int NumberOfBlocksToSpawn => _blockProperties.ValueRO.NumberOfBLocksToSpawn;
        public Entity BlockPrefab => _blockProperties.ValueRO.BlockPrefab;

        public LocalTransform GetRandomBlockTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = quaternion.identity,
                Scale = 1f
            };
        }

        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = _blockFieldRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);

            }
            while (math.distancesq(Transform.Position, randomPosition) <= NEUTRAL_ZONE_RADIUS);

            return randomPosition;
        }
        private float3 MinCorner => Transform.Position - HalfDimensions;
        private float3 MaxCorner => Transform.Position + HalfDimensions;
        private float3 HalfDimensions => new()
        {
            x = _blockProperties.ValueRO.BlockFieldSize.x,
            y = _blockProperties.ValueRO.BlockFieldSize.y,
            z = 0f
        };
        private const float NEUTRAL_ZONE_RADIUS = 20;
    }
}
