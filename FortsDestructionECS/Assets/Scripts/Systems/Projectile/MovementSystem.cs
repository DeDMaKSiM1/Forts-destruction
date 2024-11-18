
using Assets.Scripts.ComponentsAndTags;
using Assets.Scripts.ComponentsAndTags.Projectile;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
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

            new ProjectileMoveJob
            {
                DeltaTime = deltaTime,
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct ProjectileMoveJob : IJobEntity
    {
        public float DeltaTime;
        public float speed;
        void Execute(ProjectileAspect projectile)
        {
            projectile.Move(DeltaTime);
        }
    }
}
