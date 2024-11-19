using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DamageSystem))]
public sealed class DamageSystem : UpdateSystem
{
    private Request<DamageRequest> _damageRequest;
    private Event<DamageEvent> _damageEvents;
    public override void OnAwake()
    {
        _damageRequest = World.GetRequest<DamageRequest>();
        _damageEvents = World.GetEvent<DamageEvent>();
    }

    public override void OnUpdate(float deltaTime)
    {

        foreach (var request in _damageRequest.Consume())
        {
            ApplyDamage(request.TargetEntityId, request.Damage);

            _damageEvents.NextFrame(new DamageEvent
            {
                TargetEntityId = request.TargetEntityId,
            });
        }
    }
    private void ApplyDamage(EntityId entityId, float damage)
    {
        if (World.TryGetEntity(entityId, out var entity))
        {
            var healthComponent = entity.GetComponent<HealthComponent>().healthPoint -= damage;
        }
    }
}