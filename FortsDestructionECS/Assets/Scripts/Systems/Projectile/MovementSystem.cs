
using Assets.Scripts.ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct MovementSystem : ISystem
    {

        [BurstCompile]
        public void OnCreate(ref SystemState state) { }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<SpeedComponent>>())
            {
                transform.ValueRW.Position += new Unity.Mathematics.float3(0, 0, speed.ValueRO.Speed * deltaTime);
            }
        }
    }

    [BurstCompile]
    public partial struct ProjectileMoveJob : IJobEntity
    {
        public float DeltaTime;
        public float BrainRadiusSq;
        public EntityCommandBuffer.ParallelWriter ECB;


        [BurstCompile]
        private void Execute(ZombieWalkAspect zombie, [ChunkIndexInQuery] int sortKey)
        {
            zombie.Walk(DeltaTime);
            if (zombie.IsInStoppingRange(float3.zero, BrainRadiusSq))
            {
                ECB.SetComponentEnabled<ZombieWalkProperties>(sortKey, zombie.Entity, false);
                ECB.SetComponentEnabled<ZombieEatProperties>(sortKey, zombie.Entity, true);
            }

        }
    }
}
