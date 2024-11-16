using Assets.Scripts.ComponentsAndTags.Projectile;
using System.Linq.Expressions;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Systems.Projectile
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct TapSpawnSystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ProjectileProperties>();
        }
        public void OnDestroy() { }

        public void OnUpdate(ref SystemState state)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var screenPosition = Input.mousePosition;
                var worldPosition = Camera.main.ScreenToWorldPoint(new float3(screenPosition.x, screenPosition.y, 2f));
                var ecb = new EntityCommandBuffer(Allocator.Temp);

                var projectilePrefab = SystemAPI.GetSingleton<ProjectileProperties>().ProjectilePrefab;
                var projectileEntity = ecb.Instantiate(projectilePrefab);

                ecb.SetComponent(projectileEntity, new LocalTransform
                {
                    Position = worldPosition,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
                ecb.Playback(state.EntityManager);
                ecb.Dispose();
            }
        }
    }
}
