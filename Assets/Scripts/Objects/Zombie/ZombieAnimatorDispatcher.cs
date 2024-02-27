using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    public sealed class ZombieAnimatorDispatcher : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject _zombie;

        public void Attack()
        {
            _zombie.GetAction(AttackerAPI.AttackAction)?.Invoke();
        }
    }
}