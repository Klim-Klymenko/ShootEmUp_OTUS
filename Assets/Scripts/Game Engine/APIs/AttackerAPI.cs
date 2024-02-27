using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public static class AttackerAPI
    {
        [Contract(typeof(IAtomicVariable<AtomicObject>))]
        public const string AttackTarget = nameof(AttackTarget);

        [Contract(typeof(IAtomicVariable<Transform>))]
        public const string AttackTargetTransform = nameof(AttackTargetTransform);
        
        [Contract(typeof(IAtomicAction))]
        public const string AttackAction = nameof(AttackAction);
    }
}