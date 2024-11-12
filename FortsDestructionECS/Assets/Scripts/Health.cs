using Unity.Entities;

namespace Assets.Scripts
{
    public struct Health : IComponentData
    {
        public int Value;
    }
    public struct ProjectileTag : IComponentData { }
    public struct BlockTag : IComponentData { }
}
