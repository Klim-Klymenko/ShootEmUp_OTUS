﻿using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Zombie_FX
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _attackClip;
        
        [SerializeField]
        private AudioClip _deathClip;
        
        private AttackSoundController _attackSoundController;
        private DeathSoundController _deathSoundController;
        
        public void Compose(Zombie_Core core)
        {
            IAtomicObservable attackEvent = core.AttackEvent;
            IAtomicObservable deathEvent = core.DeathEvent;
            
            _attackSoundController = new AttackSoundController(attackEvent, _audioSource, _attackClip);
            _deathSoundController = new DeathSoundController(deathEvent, _audioSource, _deathClip);
        }

        internal void OnEnable()
        {
            _attackSoundController.OnEnable();
            _deathSoundController.OnEnable();
        }
        
        internal void OnDisable()
        {
            _attackSoundController.OnDisable();
            _deathSoundController.OnDisable();
        }
    }
}