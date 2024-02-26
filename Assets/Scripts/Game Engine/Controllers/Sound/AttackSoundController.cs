using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class AttackSoundController
    {
        private readonly IAtomicObservable _attackEvent;
        private readonly AudioSource _audioSource;
        private readonly AudioClip _attackClip;

        public AttackSoundController(IAtomicObservable attackEvent, AudioSource audioSource, AudioClip attackClip)
        {
            _attackEvent = attackEvent;
            _audioSource = audioSource;
            _attackClip = attackClip;
        }
        
        public void OnEnable()
        {
            _attackEvent.Subscribe(OnShoot);
        }
        
        public void OnDisable()
        {
            _attackEvent.Unsubscribe(OnShoot);
        }

        private void OnShoot()
        {
            _audioSource.PlayOneShot(_attackClip);
        }
    }
}