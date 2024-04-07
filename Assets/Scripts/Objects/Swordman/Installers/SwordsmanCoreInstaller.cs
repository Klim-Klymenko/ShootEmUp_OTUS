using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using UnityEngine;

namespace Objects.Swordsman
{
    public sealed class SwordsmanCoreInstaller : EntityInstaller
    {
        [SerializeField] 
        private Transform _transform;
        
        [SerializeField] 
        private Health _health;

        [SerializeField] 
        private MovementDirection _movementDirection;

        [SerializeField]
        private MovementSpeed _movementSpeed;
        
        [SerializeField]
        private RotationSpeed _rotationSpeed;
        
        [SerializeField]
        private TeamAffiliation _teamAffiliation;
        
        [SerializeField]
        private Attack _attack;
        
        [SerializeField]
        private MoveState _moveState;
      
        [SerializeField]
        private CurrentWeapon _weapon;
        
        public override void Install(Entity entity)
        {
            entity
                .AddComponent(_health)
                .AddComponent(_movementDirection)
                .AddComponent(_movementSpeed)
                .AddComponent(_rotationSpeed)
                .AddComponent(new Rotation { Value = _transform.rotation })
                .AddComponent(new Position { Value = _transform.position })
                .AddComponent(_teamAffiliation)
                .AddComponent(_attack)
                .AddComponent(_moveState)
                .AddComponent(new Attackable())
                .AddComponent(new Target())
                .AddComponent(_weapon);
        }
    }
}