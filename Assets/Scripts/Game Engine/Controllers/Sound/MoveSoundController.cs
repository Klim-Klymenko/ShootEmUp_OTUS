using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveSoundController
    {
        private readonly IAtomicObservable _moveClipPlayEvent;
        private readonly IAtomicObservable _moveClipStopEvent;
        private readonly AudioSource _audioSource;
        private readonly AudioClip _moveClip;

        public MoveSoundController(IAtomicObservable moveClipPlayEvent, IAtomicObservable moveClipStopEvent, AudioSource audioSource, AudioClip moveClip)
        {
            _moveClipPlayEvent = moveClipPlayEvent;
            _moveClipStopEvent = moveClipStopEvent;
            _audioSource = audioSource;
            _moveClip = moveClip;
        }

        public void OnEnable()
        {
            _audioSource.clip = _moveClip;
            
            _moveClipPlayEvent.Subscribe(OnMoveClipPlay);
            _moveClipStopEvent.Subscribe(OnMoveClipStop);
        }

        public void OnDisable()
        {
            _moveClipPlayEvent.Unsubscribe(OnMoveClipPlay);
            _moveClipStopEvent.Unsubscribe(OnMoveClipStop);
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