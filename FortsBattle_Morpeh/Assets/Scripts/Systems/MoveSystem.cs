//using Scellecs.Morpeh.Systems;
//using UnityEngine;
//using Unity.IL2CPP.CompilerServices;
//using Scellecs.Morpeh;
//using Scellecs.Morpeh.Native;
//using Unity.Jobs;
//using UnityEngine.Jobs;

//[Il2CppSetOption(Option.NullChecks, false)]
//[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
//[Il2CppSetOption(Option.DivideByZeroChecks, false)]
//[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MoveSystem))]
//public sealed class MoveSystem : FixedUpdateSystem
//{

//    private Filter filter;
//    private Stash<MovementComponent> stash;

//    public override void OnAwake()
//    {
//        filter = this.World.Filter.With<DeadTag>().Build();
//        stash = this.World.GetStash<MovementComponent>();
//    }

//    public override void OnUpdate(float deltaTime)
//    {
//        var nativeFilter = this.filter.AsNative();

//        Debug.Log("Внутри Job инициализации");
//        var parallelJob2q = new MoveJob
//        {
//            entities = nativeFilter,
//            moveComponent = stash.AsNative()
//        };
//        World.JobHandle = parallelJob.Schedule(new TransformAccessArray { });
//    }
//}