
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ComponentsAndTags.Block
{
    //Определяет компонент, содержащий объект Random
    public struct BlockFieldRandom : IComponentData
    {
        //Поле, хранящее объект Random для генерации случайных чисел
        public Random Value;
    }
}
