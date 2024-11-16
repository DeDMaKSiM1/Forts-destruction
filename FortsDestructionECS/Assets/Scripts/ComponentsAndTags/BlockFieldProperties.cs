using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ComponentsAndTags
{
    //Определяет компонент, содержащий параметры поля блоков
    public struct BlockFieldProperties : IComponentData
    {
        //Размеры поля блоков
        public float2 BlockFieldSize;
        //Количество блоков для создания
        public int NumberOfBLocksToSpawn;
        //Префаб блока, представленный как сущность
        public Entity BlockPrefab;
    }

}
