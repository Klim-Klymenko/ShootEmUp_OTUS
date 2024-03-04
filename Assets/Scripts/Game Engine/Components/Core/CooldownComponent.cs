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

        private IAtomicAction _timeOverAction;
        private IAtomicValue<bool> _cooldownCondition;

        private CooldownMechanics _cooldownMechanics;
        
        public void Compose(IAtomicAction timeOverAction, IAtomicValue<bool> cooldownCondition)
        {
            _timeOverAction = timeOverAction;
            _cooldownCondition = cooldownCondition;

            _cooldownMechanics = new CooldownMechanics(timeOverAction, _cooldownInterval, cooldownCondition);
        }

        public void Update()
        {
            _cooldownMechanics.Update();
        }
    }
}