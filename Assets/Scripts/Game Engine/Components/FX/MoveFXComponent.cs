using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class MoveFXComponent : IDisposable
    {
        [SerializeField]
        private AudioClip _moveClip;
        
        [SerializeField]
        private ParticleSystem _moveParticle;
        
        private readonly AtomicEvent _moveClipPlayEvent = new();
        private readonly AtomicEvent _moveClipStopEvent = new();
        
        private readonly AtomicEvent _moveParticlePlayEvent = new();
        private readonly AtomicEvent _moveParticleStopEvent = new();
        
        private MoveTimeFXController _moveTimeSoundController;
        private MoveSoundController _moveSoundController;
        
        private MoveTimeFXController _moveTimeParticleController;
        private MoveParticleController _moveParticleController;
        
        public void Compose(AudioSource audioSource, IAtomicValue<bool> moveCondition)
        {
            AtomicValue<float> moveClipDuration = new(_moveClip.length);
            AtomicValue<float> moveParticleDuration = new(_moveParticle.main.duration);
            
            _moveTimeSoundController = new MoveTimeFXController(moveCondition, moveClipDuration, _moveClipPlayEvent, _moveClipStopEvent);
            _moveSoundController = new MoveSoundController(_moveClipPlayEvent, _moveClipStopEvent, audioSource, _moveClip);
            
            _moveTimeParticleController = new MoveTimeFXController(moveCondition, moveParticleDuration, _moveParticlePlayEvent, _moveParticleStopEvent);
            _moveParticleController = new MoveParticleController(_moveParticlePlayEvent, _moveParticleStopEvent, _moveParticle);
        }

        public void OnEnable()
        {
            _moveTimeSoundController.OnEnable();
            _moveSoundController.OnEnable();
            
            _moveTimeParticleController.OnEnable();
            _moveParticleController.OnEnable();
        }
        
        public void Update()
        {
            _moveTimeSoundController.Update();
            _moveTimeParticleController.Update();
        }
        
        public void OnDisable()
        {
            _moveSoundController.OnDisable();
            
            _moveParticleController.OnDisable();
        }

        public void Dispose()
        {
            _moveClipPlayEvent?.Dispose();
            _moveClipStopEvent?.Dispose();
            _moveParticlePlayEvent?.Dispose();
            _moveParticleStopEvent?.Dispose();
        }
    }
}