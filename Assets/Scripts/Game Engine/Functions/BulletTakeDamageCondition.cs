using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class BulletTakeDamageCondition : IAtomicFunction<Collision, bool>
    {
        public bool Invoke(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent(out AtomicObject atomicObject)) return false;
            
            return atomicObject.Is(ObjectTypes.Damageable) && atomicObject.Is(ObjectTypes.Zombie);
        }
    }
}