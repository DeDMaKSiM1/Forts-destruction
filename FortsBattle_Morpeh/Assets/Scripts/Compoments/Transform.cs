
//using Scellecs.Morpeh;
//using UnityEngine;

//public struct Transform : IAspect, IFilterExtension
//{
//    public Entity Entity { get; set; }

//    public ref Translation Translation => ref this.translation.Get(this.Entity);
//    public ref Rotation Rotation => ref this.rotation.Get(this.Entity);
//    public ref Scale Scale => ref this.scale.Get(this.Entity);

//    private Stash<Translation> translation;
//    private Stash<Rotation> rotation;
//    private Stash<Scale> scale;
//    public void OnGetAspectFactory(World world)
//    {
//        this.translation = world.GetStash<Translation>();
//        this.rotation = world.GetStash<Rotation>();
//        this.scale = world.GetStash<Scale>();
//    }
//    public FilterBuilder Extend(FilterBuilder rootFilter) => rootFilter.With<Translation>().With<Rotation>().With<Scale>();


//}
