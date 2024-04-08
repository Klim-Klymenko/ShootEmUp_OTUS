using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using EcsEngine.Extensions;
using UnityEngine;

namespace Objects.DIInstallers.Building
{
    public sealed class BuildingInstaller : EntityInstaller
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField] 
        private Vector3 _positionOffset;
        
        [SerializeField] 
        private Health _health;
        
        [SerializeField]
        private TeamAffiliation _teamAffiliation;
        
        [SerializeField]
        private UnityAudioSource _audioSource;
        
        [SerializeField]
        private TakeDamageClip _takeDamageClip;
        
        [SerializeField]
        private TakeDamageParticle _takeDamageParticle;
        
        public override void Install(Entity entity)
        {
            entity
                .AddComponent(_health)
                .AddComponent(_teamAffiliation)
                .AddComponent(new Position { Value = _transform.position + _positionOffset })
                .AddComponent(new Attackable())
                .AddComponent(_audioSource)
                .AddComponent(_takeDamageClip)
                .AddComponent(_takeDamageParticle);
        }
    }
}