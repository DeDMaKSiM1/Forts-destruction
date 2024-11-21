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
    private Event<DamageEvent> _damageEvent;

    public float gravity = -9.81f;
    public float initialVelocity = 0f;
    private float velocity;
    private float height;
    public override void OnAwake()
    {
        _damageEvent = World.GetEvent<DamageEvent>();
        velocity = initialVelocity;
        
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var events in _damageEvent.publishedChanges)
        {
            Destroy(events.TargetEntityId);
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
                    ref var blockTransfom = ref entity.GetComponent<MovementComponent>();
                    
                    //Destroy(healthComponent.gameObject);

                }
                else if (entity.Has<ProjectileComponent>())
                {
                    Destroy(healthComponent.gameObject);
                }
            }
        }
    }
}