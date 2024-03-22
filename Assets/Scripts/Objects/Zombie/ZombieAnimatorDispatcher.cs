using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    internal sealed class ZombieAnimatorDispatcher : MonoBehaviour
    {
        internal AtomicObject Target { private get; set; }
        
        [SerializeField]
        private AtomicObject _zombie;

        internal void Attack()
        {
            _zombie.InvokeAction(AttackerAPI.AttackAction, Target);
        }
    }
}