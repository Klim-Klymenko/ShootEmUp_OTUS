using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Data;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class ClosestTargetSearchRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FindTargetRequest, Target, TeamAffiliation, Position>, Exc<Inactive, ProjectileTag>> _requesterFilter;
        private readonly EcsFilterInject<Inc<TeamAffiliation, Attackable, Position>, Exc<Inactive>> _targetFilter;
        private readonly EcsWorldInject _gameObjectsWorld;
        
        private EcsPool<FindTargetRequest> _findTargetRequestPool;
        private EcsPool<Target> _targetPool;
        private EcsPool<TeamAffiliation> _teamAffiliationPool;
        private EcsPool<Position> _positionPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _findTargetRequestPool = _requesterFilter.Pools.Inc1;
            _targetPool = _requesterFilter.Pools.Inc2;
            _teamAffiliationPool = _requesterFilter.Pools.Inc3;
            _positionPool = _requesterFilter.Pools.Inc4;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int requesterId in _requesterFilter.Value)
            {
                Team requesterTeam = _teamAffiliationPool.Get(requesterId).Value;
                Vector3 requesterPosition = _positionPool.Get(requesterId).Value;
                ref EcsPackedEntity targetEntity = ref _targetPool.Get(requesterId).Value;
                
                int targetEntityId = -1;
                float closestSqrDistance = float.MaxValue;
                
                foreach (int targetId in _targetFilter.Value)
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
                
                targetEntity = _gameObjectsWorld.Value.PackEntity(targetEntityId);
                
                _findTargetRequestPool.Del(requesterId);
            }
        }
    }
}