using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class ShootFXComponent
    {
        [SerializeField]
        private AudioClip _shootClip;
        
        [SerializeField]
        private ParticleSystem _shootParticle;

        private AttackSoundController _attackSoundController;
        private ShootParticleController _shootParticleController;
        
        public void Compose(AudioSource audioSource, IAtomicObservable shootObservable)
        {
            _attackSoundController = new AttackSoundController(shootObservable, audioSource, _shootClip);
            _shootParticleController = new ShootParticleController(shootObservable, _shootParticle);
        }
        
        public void OnEnable()
        {
            _attackSoundController.OnEnable();
            _shootParticleController.OnEnable();
        }
        
        public void OnDisable()
        {
            _attackSoundController.OnDisable();
            _shootParticleController.OnDisable();
        }
    }
}