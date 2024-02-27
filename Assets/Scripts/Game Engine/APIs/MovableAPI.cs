using Atomic.Elements;
using Atomic.Extensions;
using UnityEngine;

namespace GameEngine
{
    public static class MovableAPI
    {
        [Contract(typeof(IAtomicVariable<Vector3>))]
        public const string MoveDirection = nameof(MoveDirection); 
        
        [Contract(typeof(IAtomicValue<bool>))]
        public const string MoveCondition = nameof(MoveCondition);
    }
}