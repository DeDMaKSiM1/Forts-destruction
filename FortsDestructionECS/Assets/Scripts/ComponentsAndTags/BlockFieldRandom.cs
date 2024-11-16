
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ComponentsAndTags
{
    public struct BlockFieldRandom : IComponentData
    {
        public Random Value;
    }
}
