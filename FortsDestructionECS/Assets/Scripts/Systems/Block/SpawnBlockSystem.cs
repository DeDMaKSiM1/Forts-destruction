
using Assets.Scripts.ComponentsAndTags;
using Assets.Scripts.ComponentsAndTags.Block;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    //Системный компонент, отвечающий за создание блоков в игре

    //Используется для повышения производительности за счет компиляции кода с помощью BurstCompiler
    [BurstCompile]
    //Указывает, что система должна обновляться в группе инициализации
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnBlockSystem : ISystem
    {
        //Проверяет, что компонент заданный в <> существует, прежде чем система сможет начать работу
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //Указывает, что система должна обновляться только при наличие компонента BlockFieldProperties
            state.RequireForUpdate<BlockFieldProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
        //Основной метод, выполняющий логику системы
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //Отключается после первого обновления
            state.Enabled = false;
            //Получает сущность, содержащую BlockFieldPropetries
            var blockFieldEntity = SystemAPI.GetSingletonEntity<BlockFieldProperties>();
            //Получает аспект BlockFieldAspectдля работы с данными сущностями
            var blockField = SystemAPI.GetAspect<BlockFieldAspect>(blockFieldEntity);
            //Создает временный EntityCommandBuffer для создания и управления сущностями
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            //Цикл для создания заданного количества блоков
            for (int i = 0; i < blockField.NumberOfBlocksToSpawn; i++)
            {
                //Создает новую сущность на основе префаба блока
                var newBlock = ecb.Instantiate(blockField.BlockPrefab);
                //Получает случайный Transform для нового блока
                var newBlockTransform = blockField.GetRandomBlockTransform();
                //Устанавливает компонент Transform для созданного блока
                ecb.SetComponent(newBlock, newBlockTransform);
            }
            //Применяет все команды, накопленные в EntityCommandBuffer, к менеджеру сущностей
            ecb.Playback(state.EntityManager);
        }
    }
}
