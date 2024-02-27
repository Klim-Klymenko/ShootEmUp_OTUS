using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class TakeDamageSoundController
    {
        private readonly IAtomicObservable<int> _takeDamageObservable;
        private readonly IAtomicValue<bool> _takeDamageClipCondition;
        private readonly AudioSource _audioSource;
        private readonly AudioClip _takeDamageClip;

        public TakeDamageSoundController(IAtomicObservable<int> takeDamageObservable, IAtomicValue<bool> takeDamageClipCondition,
            AudioSource audioSource, AudioClip takeDamageClip)
        {
            _takeDamageObservable = takeDamageObservable;
            _takeDamageClipCondition = takeDamageClipCondition;
            _audioSource = audioSource;
            _takeDamageClip = takeDamageClip;
        }

        public void OnEnable()
        {
            _takeDamageObservable.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            _takeDamageObservable.Unsubscribe(OnTakeDamage);
        }
        
        private void OnTakeDamage(int _)
        {
            if (!_takeDamageClipCondition.Value) return;
            
            if (_audioSource.isPlaying)
                _audioSource.Stop();
            
            _audioSource.PlayOneShot(_takeDamageClip);
        }
    }
}