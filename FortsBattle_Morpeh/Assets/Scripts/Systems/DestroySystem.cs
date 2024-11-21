using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DestroySystem))]
public sealed class DestroySystem : UpdateSystem
{
    private Filter filter;
    public override void OnAwake()
    {
        filter =  this.World.Filter.With<DeadTag>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            Destroy(entity.ID);
        }
    }
    private void Destroy(EntityId target)
    {
        if (World.TryGetEntity(target, out var entity))
        {
            if (!entity.Has<HealthComponent>())
            {
                return;
            }
            var healthComponent = entity.GetComponent<HealthComponent>();
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