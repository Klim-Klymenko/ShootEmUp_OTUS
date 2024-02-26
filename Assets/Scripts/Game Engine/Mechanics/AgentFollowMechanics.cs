using Atomic.Elements;
using UnityEngine;
using UnityEngine.AI;

namespace GameEngine
{
    public sealed class AgentFollowMechanics
    {
        private readonly IAtomicValue<Vector3> _position;
        private readonly IAtomicValue<bool> _followCondition;
        private readonly NavMeshAgent _agent;

        public AgentFollowMechanics(IAtomicValue<Vector3> position, IAtomicValue<bool> followCondition, NavMeshAgent agent)
        {
            _position = position;
            _followCondition = followCondition;
            _agent = agent;
        }
        
        public void Update()
        {
            if (!_followCondition.Value) return;
            
            _agent.SetDestination(_position.Value);
        }
    }
}