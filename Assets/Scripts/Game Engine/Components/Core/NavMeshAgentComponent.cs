using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using UnityEngine.AI;

namespace GameEngine
{
    [Serializable]
    [Is(ObjectTypes.NavMeshAgent)]
    public sealed class NavMeshAgentComponent : IDisposable
    {
        [SerializeField] 
        private NavMeshAgent _agent;
        
        private float _stoppingDistance;
        
        [SerializeField]
        [Get(AiAPI.AgentTargetTransform)]
        private AtomicVariable<Transform> _targetTransform;
        
        private readonly AndExpression _followCondition = new();
        private readonly AndExpression _moveCondition = new();

        [SerializeField]
        [HideInInspector]
        private AtomicFunction<Vector3> _targetPosition;
        
        private SwitchNavMeshAgentMechanics _switchNavMeshAgentMechanics;
        private AgentFollowMechanics _agentFollowMechanics;
        
        public IAtomicExpression<bool> MoveCondition => _moveCondition;
        
        public void Compose()
        {
            _stoppingDistance = _agent.stoppingDistance;
                
            _followCondition.Append(new AtomicFunction<bool>(() => _targetTransform.Value != null));
            _followCondition.Append(new AtomicFunction<bool>(() => _agent.isOnNavMesh));
            
            _moveCondition.Append(_followCondition);
            _moveCondition.Append(new AtomicFunction<bool>(() => _agent.remainingDistance > _stoppingDistance));
            
            _targetPosition.Compose(() => _targetTransform.Value == null ? Vector3.zero : _targetTransform.Value.position);
            
            _switchNavMeshAgentMechanics = new SwitchNavMeshAgentMechanics(_agent);
            _agentFollowMechanics = new AgentFollowMechanics(_targetPosition, _followCondition, _agent);
        }

        public void OnEnable()
        {
            _switchNavMeshAgentMechanics.OnEnable();
        }
        
        public void Update()
        {
            _agentFollowMechanics.Update();
        }

        public void OnDisable()
        {
            _switchNavMeshAgentMechanics.OnDisable();
        }

        public void Dispose()
        {
            _targetTransform?.Dispose();
        }
    }
}