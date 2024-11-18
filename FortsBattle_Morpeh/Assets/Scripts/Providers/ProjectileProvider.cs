using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ProjectileProvider : MonoProvider<MovementComponent>
{
    [SerializeField] private float damage;

    private Request<DamageRequest> _damageRequest;

    protected override void Initialize()
    {
        _damageRequest = World.Default.GetRequest<DamageRequest>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out HealthProvider healthProvider))
        {
            _damageRequest.Publish(new DamageRequest
            {
                TargetEntityId = healthProvider.Entity.ID,
                Damage = damage
            });
        }
        
    }

}