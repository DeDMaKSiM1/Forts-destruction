using Scellecs.Morpeh;

public struct DamageRequest : IRequestData
{
    public EntityId TargetEntityId;
    public float Damage;
}
