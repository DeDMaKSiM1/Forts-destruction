using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ComponentsAndTags
{
    public struct BlockFieldProperties : IComponentData
    {
        public float2 BlockFieldSize;
        public int NumberOfBLocksToSpawn;
        public Entity BlockPrefab;
    }

}
