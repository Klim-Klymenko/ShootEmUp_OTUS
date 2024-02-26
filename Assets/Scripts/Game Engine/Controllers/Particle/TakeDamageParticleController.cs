using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class TakeDamageParticleController
    {
        private readonly IAtomicObservable<int> _takeDamageEvent;
        private readonly ParticleSystem _damageParticle;

        public TakeDamageParticleController(IAtomicObservable<int> takeDamageEvent, ParticleSystem damageParticle)
        {
            _takeDamageEvent = takeDamageEvent;
            _damageParticle = damageParticle;
        }
        
        public void OnEnable()
        {
            _takeDamageEvent.Subscribe(OnTakeDamage);
        }
        
        public void OnDisable()
        {
            _takeDamageEvent.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int _)
        {
            _damageParticle.Play(withChildren: true);
        }
    }
}