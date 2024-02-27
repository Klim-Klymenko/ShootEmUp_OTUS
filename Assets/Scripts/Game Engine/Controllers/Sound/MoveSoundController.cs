using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveSoundController
    {
        private readonly IAtomicObservable _moveClipPlayObservable;
        private readonly IAtomicObservable _moveClipStopObservable;
        private readonly AudioSource _audioSource;
        private readonly AudioClip _moveClip;

        public MoveSoundController(IAtomicObservable moveClipPlayObservable,
            IAtomicObservable moveClipStopObservable, AudioSource audioSource, AudioClip moveClip)
        {
            _moveClipPlayObservable = moveClipPlayObservable;
            _moveClipStopObservable = moveClipStopObservable;
            _audioSource = audioSource;
            _moveClip = moveClip;
        }

        public void OnEnable()
        {
            _audioSource.clip = _moveClip;
            
            _moveClipPlayObservable.Subscribe(OnMoveClipPlay);
            _moveClipStopObservable.Subscribe(OnMoveClipStop);
        }

        public void OnDisable()
        {
            _moveClipPlayObservable.Unsubscribe(OnMoveClipPlay);
            _moveClipStopObservable.Unsubscribe(OnMoveClipStop);
        }
        
        private void OnMoveClipPlay()
        {
            _audioSource.Play();
        }

        private void OnMoveClipStop()
        {
            _audioSource.Stop();
        }
    }
}