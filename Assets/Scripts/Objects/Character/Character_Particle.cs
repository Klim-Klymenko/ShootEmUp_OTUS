using System;
using Atomic.Elements;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Character_Particle : IDisposable
    {
        [SerializeField]
        private ParticleSystem _damageParticle;
        
        [SerializeField]
        private ParticleSystem _attackParticle;
        
        [SerializeField]
        private ParticleSystem _moveParticle;
        
        private readonly AtomicEvent _moveParticlePlayEvent = new();
        private readonly AtomicEvent _moveParticleStopEvent = new();
        
        private MoveTimeController _moveTimeParticleController;
        
        public void Compose(Character_Core core)
        {
            IAtomicObservable<int> takeDamageObservable = core.TakeDamageObservable;
            IAtomicObservable shootObservable = core.ShootObservable;
            IAtomicValue<bool> moveCondition = core.MoveCondition;
            
            AtomicValue<float> moveParticleDuration = new(_moveParticle.main.duration);
            
            takeDamageObservable.Subscribe(_ => _damageParticle.Play(withChildren: true));
            shootObservable.Subscribe(() => _attackParticle.Play(withChildren: true));
            
            _moveTimeParticleController = new MoveTimeController(moveCondition, moveParticleDuration, _moveParticlePlayEvent, _moveParticleStopEvent);
            _moveParticlePlayEvent.Subscribe(() => _moveParticle.Play());
            _moveParticleStopEvent.Subscribe(() => _moveParticle.Stop());
        }
        
        public void Update()
        {
            _moveTimeParticleController.Update();
        }
        
        public void Dispose()
        {
            _moveParticlePlayEvent?.Dispose();
            _moveParticleStopEvent?.Dispose();
        }
    }
}