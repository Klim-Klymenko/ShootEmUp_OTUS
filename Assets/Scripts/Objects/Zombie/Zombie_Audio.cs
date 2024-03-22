using System;
using Atomic.Elements;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Zombie_Audio
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _attackClip;
        
        [SerializeField]
        private AudioClip _deathClip;
        
        private DeathSoundController _deathSoundController;
        
        public void Compose(Zombie_Core core)
        {
            IAtomicObservable attackObservable = core.AttackEvent;
            IAtomicObservable deathObservable = core.DeathEvent;
            
            attackObservable.Subscribe(() => _audioSource.PlayOneShot(_attackClip));
            _deathSoundController = new DeathSoundController(deathObservable, _audioSource, _deathClip);
        }

        internal void OnEnable()
        {
            _deathSoundController.OnEnable();
        }
        
        internal void OnDisable()
        {
            _deathSoundController.OnDisable();
        }
    }
}