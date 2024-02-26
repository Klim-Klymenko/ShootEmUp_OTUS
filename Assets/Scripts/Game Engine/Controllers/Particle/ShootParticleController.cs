using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class ShootParticleController
    {
        private readonly IAtomicObservable _shootEvent;
        private readonly ParticleSystem _shootParticle;

        public ShootParticleController(IAtomicObservable shootEvent, ParticleSystem shootParticle)
        {
            _shootEvent = shootEvent;
            _shootParticle = shootParticle;
        }
        
        public void OnEnable()
        {
            _shootEvent.Subscribe(OnShoot);
        }
        
        public void OnDisable()
        {
            _shootEvent.Unsubscribe(OnShoot);
        }

        private void OnShoot()
        {
            _shootParticle.Play(withChildren: true);
        }
    }
}