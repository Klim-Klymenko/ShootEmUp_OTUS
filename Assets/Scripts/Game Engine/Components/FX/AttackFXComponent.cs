using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class AttackFXComponent
    {
        [SerializeField]
        private AudioClip _attackClip;
        
        [SerializeField]
        private ParticleSystem _attackParticle;

        private AttackSoundController _attackSoundController;
        private AttackParticleController _attackParticleController;
        
        public void Compose(AudioSource audioSource, IAtomicObservable attackObservable)
        {
            _attackSoundController = new AttackSoundController(attackObservable, audioSource, _attackClip);
            _attackParticleController = new AttackParticleController(attackObservable, _attackParticle);
        }
        
        public void OnEnable()
        {
            _attackSoundController.OnEnable();
            _attackParticleController.OnEnable();
        }
        
        public void OnDisable()
        {
            _attackSoundController.OnDisable();
            _attackParticleController.OnDisable();
        }
    }
}