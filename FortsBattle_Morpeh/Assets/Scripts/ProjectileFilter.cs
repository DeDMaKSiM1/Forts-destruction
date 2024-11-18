using Scellecs.Morpeh;
using UnityEngine;

public struct ProjectileFilter : IFilterExtension
{
    public FilterBuilder Extend(FilterBuilder rootFilter) => rootFilter.With<HealthComponent>().With<ProjectileTag>();

}
