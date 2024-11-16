using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;
using Assets.Scripts.ComponentsAndTags;

namespace Assets.Scripts
{
    public class BlockFieldMono : MonoBehaviour
    {
        public float2 BlockFieldSize;
        public int NumberOfBlocksToSpawn;
        public GameObject BlockPrefab;
        public uint RandomSeed;
    }
    public class BlockFieldBaker : Baker<BlockFieldMono>
    {
        public override void Bake(BlockFieldMono authoring)
        {
            var blockFieldEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(blockFieldEntity, new BlockFieldProperties
            {
                BlockFieldSize = authoring.BlockFieldSize,
                NumberOfBLocksToSpawn = authoring.NumberOfBlocksToSpawn,
                BlockPrefab = GetEntity(authoring.BlockPrefab, TransformUsageFlags.Dynamic)
            });

            AddComponent(blockFieldEntity, new BlockFieldRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
        }
    }
}
