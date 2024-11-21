using Scellecs.Morpeh.Native;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public struct MoveJob : IJobParallelFor
{
    [ReadOnly]
    public NativeFilter entities;
    public NativeStash<MovementComponent> moveComponent;
    public void Execute(int index)
    {
        var entityId = this.entities[index];

        ref var component = ref this.moveComponent.Get(entityId);
        component.position = new UnityEngine.Vector2(0, 0);
        Debug.Log($"Сущность {entityId} должна переместиться на 0,0");
    }
}
