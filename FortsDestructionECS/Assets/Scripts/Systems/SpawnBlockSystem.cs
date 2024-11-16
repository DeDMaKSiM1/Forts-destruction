
using Assets.Scripts.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnBlockSystem : ISystem
    {
        [BurstCompile]

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BlockFieldProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var blockFieldEntity = SystemAPI.GetSingletonEntity<BlockFieldProperties>();
            var blockField = SystemAPI.GetAspect<BlockFieldAspect>(blockFieldEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            for (int i = 0; i < blockField.NumberOfBlocksToSpawn; i++)
            {
                var newBlock = ecb.Instantiate(blockField.BlockPrefab);
                var newBlockTransform = blockField.GetRandomBlockTransform();
                ecb.SetComponent(newBlock, newBlockTransform);
            }
            ecb.Playback(state.EntityManager);
        }
    }
}
