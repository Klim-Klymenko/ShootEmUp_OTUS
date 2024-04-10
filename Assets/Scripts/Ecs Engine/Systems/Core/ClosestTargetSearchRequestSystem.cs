using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class ClosestTargetSearchRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FindTargetRequest, Target, TeamAffiliation, Position>, Exc<Inactive>> _requesterFilterInject;
        private readonly EcsFilterInject<Inc<TeamAffiliation, Attackable, Position>, Exc<Inactive>> _targetFilterInject;
        private readonly EcsWorldInject _gameObjectsWorldInject;
        
        private EcsPool<FindTargetRequest> _findTargetRequestPool;
        private EcsPool<Target> _targetPool;
        private EcsPool<TeamAffiliation> _teamAffiliationPool;
        private EcsPool<Position> _positionPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _findTargetRequestPool = _requesterFilterInject.Pools.Inc1;
            _targetPool = _requesterFilterInject.Pools.Inc2;
            _teamAffiliationPool = _requesterFilterInject.Pools.Inc3;
            _positionPool = _requesterFilterInject.Pools.Inc4;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int requesterId in _requesterFilterInject.Value)
            {
                Team requesterTeam = _teamAffiliationPool.Get(requesterId).Value;
                Vector3 requesterPosition = _positionPool.Get(requesterId).Value;
                ref EcsPackedEntity targetEntity = ref _targetPool.Get(requesterId).Value;
                
                int targetEntityId = -1;
                float closestSqrDistance = float.MaxValue;
                
                foreach (int targetId in _targetFilterInject.Value)
                {
                    Team targetTeam = _teamAffiliationPool.Get(targetId).Value;
                    Vector3 targetPosition = _positionPool.Get(targetId).Value;
                    
                    if (requesterTeam == targetTeam) continue;
                    
                    Vector3 distanceVector = targetPosition - requesterPosition;
                    float sqrDistance = distanceVector.sqrMagnitude;
                    
                    if (closestSqrDistance < sqrDistance) continue;
                    
                        closestSqrDistance = sqrDistance;
                        targetEntityId = targetId;
                }
                
                targetEntity = _gameObjectsWorldInject.Value.PackEntity(targetEntityId);
                
                _findTargetRequestPool.Del(requesterId);
            }
        }
    }
}