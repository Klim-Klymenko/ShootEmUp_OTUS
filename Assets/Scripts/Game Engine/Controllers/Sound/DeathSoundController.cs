using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class DeathSoundController
    {
        private readonly IAtomicObservable _deathObservable;
        private readonly AudioSource _audioSource;
        private readonly AudioClip _deathClip;

        public DeathSoundController(IAtomicObservable deathObservable, AudioSource audioSource, AudioClip deathClip)
        {
            _deathObservable = deathObservable;
            _audioSource = audioSource;
            _deathClip = deathClip;
        }

        public void OnEnable()
        {
            _deathObservable.Subscribe(OnDeath);
        }
        
        public void OnDisable()
        {
            _deathObservable.Unsubscribe(OnDeath);
        }

        private void OnDeath()
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
            
            _audioSource.PlayOneShot(_deathClip);
        }
    }
}