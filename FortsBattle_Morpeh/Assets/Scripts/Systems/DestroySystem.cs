using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System.Collections.Generic;
using Scellecs.Morpeh.Native;
using UnityEngine.Jobs;
using Unity;
using Unity.Mathematics;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DestroySystem))]
public sealed class DestroySystem : FixedUpdateSystem
{
    private Filter deadFilter;

    private const float gravity = 9.81f;

    
    public override void OnAwake()
    {
        deadFilter = this.World.Filter.With<DeadTag>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in deadFilter)
        {
            ref var entityMoveComp = ref entity.GetComponent<MovementComponent>();
            ref Transform body = ref entityMoveComp.Transform;
            ref Vector3 velocity = ref entityMoveComp.Velocity;
            velocity.x = 1f;
            velocity.y = 0.5f;
            //rand = new Unity.Mathematics.Random(100);
            //float directionX = rand.NextFloat(-1f, 1f);
            //float directionY = rand.NextFloat(1f, 3f);

            //velocity.x = directionX * 1f;
            //velocity.y = directionX * 0.5f;


            body.position += new Vector3(velocity.x, velocity.y, 0f);
            //Destroy(entity.ID);

        }

        //var nativeFilter = this.filter.AsNative();

        //Debug.Log("Внутри Job инициализации");
        //var parallelJob = new MoveJob
        //{
        //    entities = nativeFilter,
        //    moveComponent = stash.AsNative(),
        //};
        //Debug.Log("Инициализация успешна");

        //var TransArrayAccess = new TransformAccessArray();

        //World.JobHandle = parallelJob.Schedule(TransArrayAccess);
    }
    private void Destroy(EntityId target)
    {
        if (World.TryGetEntity(target, out var entity))
        {
            if (!entity.Has<HealthComponent>())
            {
                return;
            }
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();
            if (healthComponent.healthPoint <= 0)
            {

                if (entity.Has<BlockComponent>())
                {
                    //ref var blockTransfom = ref entity.GetComponent<MovementComponent>();

                    Destroy(healthComponent.gameObject);

                }
                else if (entity.Has<ProjectileComponent>())
                {
                    Destroy(healthComponent.gameObject);
                }
            }
        }
    }
}