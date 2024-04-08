using EcsEngine.Components;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace EcsEngine.Physics
{
    internal sealed class EcsCollisionComponent : MonoBehaviour
    {
        [SerializeField]
        private Entity _entity;
        
        private EcsEntityBuilder _entityBuilder;
        
        [Inject]
        internal void Construct(EcsEntityBuilder entityBuilder)
        {
            _entityBuilder = entityBuilder;
        }

        private void OnCollisionEnter(Collision other)
        {
            HandleCollision(other.collider);
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleCollision(other);
        }

        private void HandleCollision(Component other)
        {
            if (!TryGetCollisionInfo(other, out EcsPackedEntity targetEntity)) return;

            _entityBuilder.CreateEntity()
                .Add(new Source { Value = _entity.PackedEntity })
                .Add(new Target { Value = targetEntity })
                .Add(new CollisionRequest());
        }
        
        private bool TryGetCollisionInfo(Component other, out EcsPackedEntity targetEntity)
        {
            targetEntity = default;
            
            if (!other.gameObject.TryGetComponent(out Entity entity)) return false;
            targetEntity = entity.PackedEntity;

            return true;
        }
    }
}