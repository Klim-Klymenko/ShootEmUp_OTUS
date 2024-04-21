using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Objects.Projectile
{
    internal sealed class ProjectileInstaller : EntityInstaller
    {
        [SerializeField]
        private Transform _transform;
        
        [SerializeField] 
        private MovementDirection _movementDirection;

        [SerializeField]
        private MovementSpeed _movementSpeed;

        [SerializeField]
        private RotationSpeed _rotationSpeed;
        
        [SerializeField]
        private UnityTransform _unityTransform;
        
        [SerializeField]
        private TeamAffiliation _teamAffiliation;
        
        public override void Install(Entity entity, EcsWorld world)
        {
            entity
                .AddComponent(new Position { Value = _transform.position })
                .AddComponent(new Rotation { Value = _transform.rotation })
                .AddComponent(_movementDirection)
                .AddComponent(_movementSpeed)
                .AddComponent(_rotationSpeed)
                .AddComponent(_unityTransform)
                .AddComponent(new ProjectileTag())
                .AddComponent(new TargetSearchDisabled())
                .AddComponent(new MoveEnabled())
                .AddComponent(new SpawnAdjustable());
        }
    }
}