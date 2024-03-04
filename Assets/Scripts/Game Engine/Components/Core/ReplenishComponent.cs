using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class ReplenishComponent : IDisposable
    {
        private readonly AtomicEvent _replenishEvent = new();

        [SerializeField] 
        private CooldownComponent _cooldownComponent;
        
        private ReplenishMechanics _replenishMechanics;

        public void Compose(IAtomicVariable<int> charges, IAtomicValue<bool> aliveCondition)
        {
            _cooldownComponent.Compose(_replenishEvent, aliveCondition);
            _replenishMechanics = new ReplenishMechanics(_replenishEvent, charges);
        }
        
        public void OnEnable()
        {
            _replenishMechanics.OnEnable();
        }
        
        public void Update()
        {
            _cooldownComponent.Update();
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