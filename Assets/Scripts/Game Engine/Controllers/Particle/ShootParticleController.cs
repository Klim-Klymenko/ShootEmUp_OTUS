using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class ShootParticleController
    {
        private readonly IAtomicObservable _shootObservable;
        private readonly ParticleSystem _shootParticle;

        public ShootParticleController(IAtomicObservable shootObservable, ParticleSystem shootParticle)
        {
            _shootObservable = shootObservable;
            _shootParticle = shootParticle;
        }
        
        public void OnEnable()
        {
            _shootObservable.Subscribe(OnShoot);
        }
        
        public void OnDisable()
        {
            _shootObservable.Unsubscribe(OnShoot);
        }

        private void OnShoot()
        {
            _shootParticle.Play(withChildren: true);
        }
    }
}