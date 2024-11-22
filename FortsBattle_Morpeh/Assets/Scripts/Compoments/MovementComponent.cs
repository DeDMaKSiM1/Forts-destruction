using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct MovementComponent : IComponent
{
    public Transform Transform;
    public Vector3 Velocity;
    public float ThrowForce;
    public Vector3 GetPosition() => Transform.position;
}