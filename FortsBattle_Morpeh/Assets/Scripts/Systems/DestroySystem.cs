using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System.Collections.Generic;
using Scellecs.Morpeh.Native;
using UnityEngine.Jobs;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DestroySystem))]
public sealed class DestroySystem : UpdateSystem
{
    private Filter deadFilter;
    private Filter filter;
    //private AspectFactory<Transform> transform;
    private Stash<MovementComponent> stash;
    public override void OnAwake()
    {
        deadFilter = this.World.Filter.With<DeadTag>().Build();
        //this.filter = this.World.Filter.Extend<Transform>().Build();
        stash = this.World.GetStash<MovementComponent>();
        //this.transform = this.World.GetAspectFactory<Transform>();
    }

    public override void OnUpdate(float deltaTime)
    {
        //var transformList = new List<Transform>();

        foreach (var entity in deadFilter)
        {
            Transform body = entity.GetComponent<MovementComponent>().Transform;
            body.position = new Vector3(0, 0);
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