using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class CooldownComponent
    {
        [SerializeField]
        private AtomicValue<float> _cooldownInterval;
        
        private readonly AndExpression _cooldownCondition = new();

        private CooldownMechanics _cooldownMechanics;
        
        public IAtomicExpression<bool> CoolDownCondition => _cooldownCondition;
        
        public void Compose(IAtomicAction timeOverAction)
        {
            _cooldownMechanics = new CooldownMechanics(timeOverAction, _cooldownInterval, _cooldownCondition);
        }

        public void Update()
        {
            _cooldownMechanics.Update();
        }
    }
}