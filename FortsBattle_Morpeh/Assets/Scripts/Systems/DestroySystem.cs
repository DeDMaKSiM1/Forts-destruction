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
            Destroy(entity);
        }
    }

    private void Destroy(Entity entity)
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
                ref var blockComp = ref entity.GetComponent<BlockComponent>();
                blockComp.rb.bodyType = RigidbodyType2D.Dynamic; 
                healthComponent.gameObject.layer = 6;

                blockComp.rb.AddForce(new(-1  * 200f, 202));


                Destroy(healthComponent.gameObject, 3);
                entity.RemoveComponent<DeadTag>();
            }
            else if (entity.Has<ProjectileComponent>())
            {
                Destroy(healthComponent.gameObject);
                entity.RemoveComponent<DeadTag>();

            }
            else
                return;
        }

    }

}