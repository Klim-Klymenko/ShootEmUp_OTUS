using EcsEngine.Components.Events;
using EcsEngine.Extensions;
using UnityEngine;

namespace Common.GameEngine
{
    internal sealed class UnitAnimatorDispatcher : MonoBehaviour
    {
        [SerializeField] 
        private Entity _entity;
        
        internal void OnAttackAnimation()
        {
            _entity.AddComponent(new AttackEvent());
        }
    }
}