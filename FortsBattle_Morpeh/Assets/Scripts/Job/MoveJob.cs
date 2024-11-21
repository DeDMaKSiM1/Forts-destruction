using Scellecs.Morpeh.Native;
using Unity.Burst;
using Unity.Jobs;

[BurstCompile]
public struct MoveJob : IJobParallelFor
{
    [Unity.Collections.ReadOnly]
    public NativeFilter entities;
    public NativeStash<MovementComponent> moveComponent;
    public void Execute(int index)
    {
        var entityId = this.entities[index];

        ref var component = ref this.moveComponent.Get(entityId);
        component.position = new UnityEngine.Vector2(0, 0);
    }
}
