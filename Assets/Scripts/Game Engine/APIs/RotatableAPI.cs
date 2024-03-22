using Atomic.Elements;
using Atomic.Extensions;
using UnityEngine;

namespace GameEngine
{
    public static class RotatableAPI
    {
        [Contract(typeof(IAtomicVariable<Vector3>))]
        public const string RotateDirection = nameof(RotateDirection);
    }
}