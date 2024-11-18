using Assets.Scripts.ComponentsAndTags;
using Assets.Scripts.ComponentsAndTags.Projectile;
using System.Linq.Expressions;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Systems.Projectile
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct TapSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ProjectileProperties>();
        }
        [BurstCompile]
        public void OnDestroy() { }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var projectileEntity = SystemAPI.GetSingletonEntity<ProjectileProperties>();
                var projectile = SystemAPI.GetAspect<ProjectileAspect>(projectileEntity);

                var ecb = new EntityCommandBuffer(Allocator.Temp);

                var newProjectile = ecb.Instantiate(projectile.ProjectilePrefab);
                var newProjectileTransform = projectile.GetProjectileTransform();

                ecb.SetComponent(newProjectile, newProjectileTransform);



                ecb.Playback(state.EntityManager);
                ecb.Dispose();
            }
        }
    }


}
