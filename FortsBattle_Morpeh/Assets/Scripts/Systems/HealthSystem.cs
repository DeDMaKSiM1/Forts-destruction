using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthSystem))]
public sealed class HealthSystem : UpdateSystem
{
    //��������� �������, ��� ���������� ���� ��������� � ����������� HealthComponent
    private Filter filter;
    //������� ����� Stash, ��� ���������� ������������������
    public override void OnAwake()
    {
        this.filter = this.World.Filter.Extend<ProjectileFilter>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();
        }
    }
}