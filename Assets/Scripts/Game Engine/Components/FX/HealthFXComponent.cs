using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class HealthFXComponent
    {
        [SerializeField] 
        private AudioClip _takeDamageClip;

        [SerializeField]
        private AudioClip _deathClip;

        [SerializeField]
        private ParticleSystem _damageParticle;
        
        private TakeDamageSoundController _takeDamageSoundController;
        private DeathSoundController _deathSoundController;

        private TakeDamageParticleController _takeDamageParticleController;
        
        public void Compose(AudioSource audioSource, IAtomicObservable<int> takeDamageEvent, 
            IAtomicObservable deathEvent, IAtomicValue<bool> takeDamageClipCondition)
        {
            _takeDamageSoundController = new TakeDamageSoundController(takeDamageEvent, takeDamageClipCondition, audioSource, _takeDamageClip);
            _deathSoundController = new DeathSoundController(deathEvent, audioSource, _deathClip);
            
            _takeDamageParticleController = new TakeDamageParticleController(takeDamageEvent, _damageParticle);
        }

        public void OnEnable()
        {
            _takeDamageSoundController.OnEnable();
            _deathSoundController.OnEnable();
            
            _takeDamageParticleController.OnEnable();
        }

        public void OnDisable()
        {
            _takeDamageSoundController.OnDisable();
            _deathSoundController.OnDisable();
            
            _takeDamageParticleController.OnDisable();
        }
    }
}