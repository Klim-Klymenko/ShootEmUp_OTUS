using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public static class ObjectAPI
    {
        [Contract(typeof(IAtomicVariable<Vector3>))]
        public const string MoveDirection = nameof(MoveDirection); 
        
        [Contract(typeof(IAtomicValue<bool>))]
        public const string IsMoving = nameof(IsMoving);
        
        [Contract(typeof(IAtomicValue<bool>))]
        public const string IsAlive = nameof(IsAlive);
        
        [Contract(typeof(IAtomicAction))]
        public const string ShootAction = nameof(ShootAction);

        [Contract(typeof(IAtomicValue<int>))] 
        public const string ShootingInterval = nameof(ShootingInterval);
        
        [Contract(typeof(IAtomicValue<int>))]
        public const string HitPoints = nameof(HitPoints);
        
        [Contract(typeof(IAtomicObservable))]
        public const string DeathObservable = nameof(DeathObservable);
        
        [Contract(typeof(IAtomicAction))]
        public const string AttackAction = nameof(AttackAction);
        
        [Contract(typeof(IAtomicVariable<AtomicObject>))]
        public const string AgentTarget = nameof(AgentTarget);
        
        [Contract(typeof(IAtomicVariable<Transform>))]
        public const string AgentTargetTransform = nameof(AgentTargetTransform);

        [Contract(typeof(IAtomicVariable<AtomicObject>))]
        public const string AttackTarget = nameof(AttackTarget);

        [Contract(typeof(IAtomicVariable<Transform>))]
        public const string AttackTargetTransform = nameof(AttackTargetTransform);

        [Contract(typeof(IAtomicAction<int>))]
        public const string TakeDamageAction = nameof(TakeDamageAction);
        
        [Contract(typeof(SkinnedMeshRenderer))]
        public const string SkinnedMeshRenderer = nameof(SkinnedMeshRenderer);
    }
}