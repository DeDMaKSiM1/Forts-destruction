using Assets.Scripts.ComponentsAndTags;
using Assets.Scripts.ComponentsAndTags.Projectile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.AuthoringAndMono
{
    public class ProjectileMono : MonoBehaviour
    {
        public GameObject ProjectilePrefab;
        public float Speed;
    }

    public class ProjectileBaker : Baker<ProjectileMono>
    {
        public override void Bake(ProjectileMono authoring)
        {
            var projectileEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(projectileEntity, new ProjectileProperties
            {
                ProjectilePrefab = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic),
                Speed = authoring.Speed,
            });

        }
    }
}
