using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveParticleController
    {
        private readonly IAtomicObservable _moveParticlePlayEvent;
        private readonly IAtomicObservable _moveParticleStopEvent;
        private readonly ParticleSystem _moveParticle;

        public MoveParticleController(IAtomicObservable moveParticlePlayEvent, IAtomicObservable moveParticleStopEvent, ParticleSystem moveParticle)
        {
            _moveParticlePlayEvent = moveParticlePlayEvent;
            _moveParticleStopEvent = moveParticleStopEvent;
            _moveParticle = moveParticle;
        }
        
        public void OnEnable()
        {
            _moveParticlePlayEvent.Subscribe(OnMoveParticlePlay);
            _moveParticleStopEvent.Subscribe(OnMoveParticleStop);
        }

        public void OnDisable()
        {
            _moveParticlePlayEvent.Unsubscribe(OnMoveParticlePlay);
            _moveParticleStopEvent.Unsubscribe(OnMoveParticleStop);
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