using Scellecs.Morpeh.Native;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

[BurstCompile]
public struct MoveJob : IJobParallelForTransform
{
    [ReadOnly]
    public NativeFilter entities;
    //public NativeStash<MovementComponent> moveComponent;

    public void Execute(int index, TransformAccess transform)
    {
        var entityId = this.entities[index];

        transform.position = new Vector2(0, 0);
        Debug.Log($"Сущность {entityId} должна переместиться на 0,0");
    }
}

