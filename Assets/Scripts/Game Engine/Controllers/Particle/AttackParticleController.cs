using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class AttackParticleController
    {
        private readonly IAtomicObservable _attackObservable;
        private readonly ParticleSystem _attackParticle;

        public AttackParticleController(IAtomicObservable attackObservable, ParticleSystem attackParticle)
        {
            _attackObservable = attackObservable;
            _attackParticle = attackParticle;
        }
        
        public void OnEnable()
        {
            _attackObservable.Subscribe(OnAttack);
        }
        
        public void OnDisable()
        {
            _attackObservable.Unsubscribe(OnAttack);
        }

        private void OnAttack()
        {
            _attackParticle.Play(withChildren: true);
        }
    }
}