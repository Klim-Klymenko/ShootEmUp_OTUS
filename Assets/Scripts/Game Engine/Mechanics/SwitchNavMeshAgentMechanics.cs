using UnityEngine.AI;

namespace GameEngine
{
    public sealed class SwitchNavMeshAgentMechanics
    {
        private readonly NavMeshAgent _agent;

        public SwitchNavMeshAgentMechanics(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public void OnEnable()
        {
            if (_agent.enabled) return;
            
            _agent.enabled = true;
        }

        public void OnDisable()
        {
            if (_agent == null || !_agent.enabled) return;
            
            _agent.enabled = false;
        }
    }
}