using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public sealed class PassOnTargetMechanics
    {
        private readonly IAtomicFunction<Collision, bool> _passCondition;
        private readonly IAtomicAction<AtomicObject> _onCollisionAction;

        public PassOnTargetMechanics(IAtomicFunction<Collision, bool> passCondition, IAtomicAction<AtomicObject> onCollisionAction)
        {
            _passCondition = passCondition;
            _onCollisionAction = onCollisionAction;
        }

        public void OnCollisionEnter(Collision other)
        {
            if (!_passCondition.Invoke(other)) return;
            
            AtomicObject target = other.gameObject.GetComponent<AtomicObject>();
            _onCollisionAction?.Invoke(target);
        }
    }
}