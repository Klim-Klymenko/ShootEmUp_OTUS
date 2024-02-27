using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class TakeDamageParticleController
    {
        private readonly IAtomicObservable<int> _takeDamageObservable;
        private readonly ParticleSystem _damageParticle;

        public TakeDamageParticleController(IAtomicObservable<int> takeDamageObservable, ParticleSystem damageParticle)
        {
            _takeDamageObservable = takeDamageObservable;
            _damageParticle = damageParticle;
        }
        
        public void OnEnable()
        {
            _takeDamageObservable.Subscribe(OnTakeDamage);
        }
        
        public void OnDisable()
        {
            _takeDamageObservable.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int _)
        {
            _damageParticle.Play(withChildren: true);
        }
    }
}