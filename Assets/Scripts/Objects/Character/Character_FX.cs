using System;
using Atomic.Elements;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Character_FX : IDisposable
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioSource _moveAudioSource;
        
        [SerializeField]
        private HealthFXComponent _healthFXComponent;

        [SerializeField] 
        private AttackFXComponent _attackFXComponent;

        [SerializeField] 
        private MoveFXComponent _moveFXComponent;
        
        internal void Compose(Character_Core core)
        {
            IAtomicObservable<int> takeDamageEvent = core.TakeDamageObservable;
            IAtomicObservable deathEvent = core.DeathObservable;
            IAtomicObservable shootEvent = core.ShootObservable;
            IAtomicValue<bool> takeDamageClipCondition = core.AliveCondition;
            IAtomicValue<bool> moveCondition = core.MoveCondition;
            
            _healthFXComponent.Compose(_audioSource, takeDamageEvent, deathEvent, takeDamageClipCondition);
            _attackFXComponent.Compose(_audioSource, shootEvent);
            _moveFXComponent.Compose(_moveAudioSource, moveCondition);
        }
        
        internal void OnEnable()
        {
            _healthFXComponent.OnEnable();
            _attackFXComponent.OnEnable();
            _moveFXComponent.OnEnable();
        }

        internal void Update()
        {
            _moveFXComponent.Update();
        }
        
        internal void OnDisable()
        {
            _healthFXComponent.OnDisable();
            _attackFXComponent.OnDisable();
            _moveFXComponent.OnDisable();
        }

        public void Dispose()
        {
            _moveFXComponent?.Dispose();
        }
    }
}