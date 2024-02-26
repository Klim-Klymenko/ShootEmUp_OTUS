using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class ReplenishComponent : IDisposable
    {
        [SerializeField]
        private AtomicValue<float> _replenishInterval;
        
        private readonly AtomicEvent _replenishEvent = new();
        
        private CooldownMechanics _cooldownMechanics;
        private ReplenishMechanics _replenishMechanics;

        public void Compose(IAtomicVariable<int> charges, IAtomicValue<bool> replenishCondition)
        {
            _cooldownMechanics = new CooldownMechanics(_replenishEvent, _replenishInterval, replenishCondition);
            _replenishMechanics = new ReplenishMechanics(_replenishEvent, charges);
        }
        
        public void OnEnable()
        {
            _replenishMechanics.OnEnable();
        }
        
        public void Update()
        {
            _cooldownMechanics.Update();
        }
        
        public void OnDisable()
        {
            _replenishMechanics.OnDisable();
        }

        public void Dispose()
        {
            _replenishEvent?.Dispose();
        }
    }
}