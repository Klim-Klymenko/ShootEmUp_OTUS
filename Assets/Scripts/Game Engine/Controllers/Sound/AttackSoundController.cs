﻿using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public sealed class AttackSoundController
    {
        private readonly IAtomicObservable _attackObservable;
        private readonly AudioSource _audioSource;
        private readonly AudioClip _attackClip;

        public AttackSoundController(IAtomicObservable attackObservable, AudioSource audioSource, AudioClip attackClip)
        {
            _attackObservable = attackObservable;
            _audioSource = audioSource;
            _attackClip = attackClip;
        }
        
        public void OnEnable()
        {
            _attackObservable.Subscribe(OnAttack);
        }
        
        public void OnDisable()
        {
            _attackObservable.Unsubscribe(OnAttack);
        }

        private void OnAttack()
        {
            _audioSource.PlayOneShot(_attackClip);
        }
    }
}