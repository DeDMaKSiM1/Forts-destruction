

using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.ComponentsAndTags
{
    public readonly partial struct BlockFieldAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;

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

            randomPosition = _blockFieldRandom.ValueRW.Value.NextFloat3(-7, 7);
            return randomPosition;
        }
    }
}
