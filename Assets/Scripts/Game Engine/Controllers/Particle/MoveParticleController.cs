using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveParticleController
    {
        private readonly IAtomicObservable _moveParticlePlayObservable;
        private readonly IAtomicObservable _moveParticleStopObservable;
        private readonly ParticleSystem _moveParticle;

        public MoveParticleController(IAtomicObservable moveParticlePlayObservable,
            IAtomicObservable moveParticleStopObservable, ParticleSystem moveParticle)
        {
            _moveParticlePlayObservable = moveParticlePlayObservable;
            _moveParticleStopObservable = moveParticleStopObservable;
            _moveParticle = moveParticle;
        }
        
        public void OnEnable()
        {
            _moveParticlePlayObservable.Subscribe(OnMoveParticlePlay);
            _moveParticleStopObservable.Subscribe(OnMoveParticleStop);
        }

        public void OnDisable()
        {
            _moveParticlePlayObservable.Unsubscribe(OnMoveParticlePlay);
            _moveParticleStopObservable.Unsubscribe(OnMoveParticleStop);
        }
        
        private void OnMoveParticlePlay()
        {
            _moveParticle.Play();
        }
        
        private void OnMoveParticleStop()
        {
            _moveParticle.Stop();
        }
    }
}