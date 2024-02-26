using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class DeathSoundController
    {
        private readonly IAtomicObservable _deathEvent;
        private readonly AudioSource _audioSource;
        private readonly AudioClip _deathClip;

        public DeathSoundController(IAtomicObservable deathEvent, AudioSource audioSource, AudioClip deathClip)
        {
            _deathEvent = deathEvent;
            _audioSource = audioSource;
            _deathClip = deathClip;
        }

        public void OnEnable()
        {
            _deathEvent.Subscribe(OnDeath);
        }
        
        public void OnDisable()
        {
            _deathEvent.Unsubscribe(OnDeath);
        }

        private void OnDeath()
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
            
            _audioSource.PlayOneShot(_deathClip);
        }
    }
}