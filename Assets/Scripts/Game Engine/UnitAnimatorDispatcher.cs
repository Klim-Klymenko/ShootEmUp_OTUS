using EcsEngine.Components.Events;
using EcsEngine.Extensions;
using UnityEngine;

namespace Common.GameEngine
{
    public sealed class UnitAnimatorDispatcher : MonoBehaviour
    {
        [SerializeField] 
        private Entity _entity;
        
        public void OnAttackAnimation()
        {
            _entity.AddComponent(new AttackEvent());
        }
    }
}