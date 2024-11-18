using Unity.Entities;

namespace Assets.Scripts.ComponentsAndTags.Projectile
{
    public struct ProjectileProperties : IComponentData
    {
        public Entity ProjectilePrefab;
        public float Speed;

    }
}
