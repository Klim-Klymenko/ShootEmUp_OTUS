using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal class Zombie_AI
    {
        [Section]
        [SerializeField]
        private NavMeshAgentComponent _agentComponent;

        public IAtomicValue<bool> MoveCondition => _agentComponent.MoveCondition;
        
        internal void Compose(Zombie_Core core)
        {
            _agentComponent.Let(it =>
            {
                it.Compose();
                it.MoveCondition.Append(core.AliveCondition);
            });
        }

        public void OnEnable()
        {
            _agentComponent.OnEnable();
        }
        
        public void Update()
        {
            _agentComponent.Update();
        }
        
        public void OnDisable()
        {
            _agentComponent.OnDisable();
        }
        
        public void Dispose()
        {
            _agentComponent?.Dispose();
        }
    }
}