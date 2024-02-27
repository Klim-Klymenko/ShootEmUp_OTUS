using System;
using Atomic.Elements;
using GameEngine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Objects
{
    [Serializable]
    public sealed class Character_FX : IDisposable
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioSource _moveAudioSource;
        
        [SerializeField]
        private HealthFXComponent _healthFXComponent;

        [SerializeField] 
        private ShootFXComponent _shootFXComponent;

        [SerializeField] 
        private MoveFXComponent _moveFXComponent;
        
        public void Compose(Character_Core core)
        {
            IAtomicObservable<int> takeDamageEvent = core.TakeDamageObservable;
            IAtomicObservable deathEvent = core.DeathObservable;
            IAtomicObservable shootEvent = core.ShootObservable;
            IAtomicValue<bool> takeDamageClipCondition = core.AliveCondition;
            IAtomicValue<bool> moveCondition = core.MoveCondition;
            
            _healthFXComponent.Compose(_audioSource, takeDamageEvent, deathEvent, takeDamageClipCondition);
            _shootFXComponent.Compose(_audioSource, shootEvent);
            _moveFXComponent.Compose(_moveAudioSource, moveCondition);
        }
        
        public void OnEnable()
        {
            _healthFXComponent.OnEnable();
            _shootFXComponent.OnEnable();
            _moveFXComponent.OnEnable();
        }

        public void Update()
        {
            _moveFXComponent.Update();
        }
        
        public void OnDisable()
        {
            _healthFXComponent.OnDisable();
            _shootFXComponent.OnDisable();
            _moveFXComponent.OnDisable();
        }

        public void Dispose()
        {
            _moveFXComponent?.Dispose();
        }
    }
}